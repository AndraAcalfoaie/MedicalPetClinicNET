using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Exceptions;
using Services.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public interface IUserService
    {
        AuthenticateResponse Register(RegisterUserDto user);
        AuthenticateResponse Login(LoginModelDto loginModel);
        User GetById(int userId);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public AuthenticateResponse Login(LoginModelDto loginModel)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            if (dbUser is null)
                throw new ValidationException("Invalid email or password");

            var loginResult = _mapper.Map<AuthenticateResponse>(dbUser);
            loginResult.JwtToken = GenerateJwtToken(dbUser);

            return loginResult;
        }

        public AuthenticateResponse Register(RegisterUserDto user)
        {
            if (_dbContext.Users.Any(u => u.Email == user.Email))
                throw new ValidationException("Email already used");

            var dbUser = _mapper.Map<User>(user);
            dbUser.IsAdmin = false;

            _dbContext.Users.Add(dbUser);
            _dbContext.SaveChanges();

            var loginResult = _mapper.Map<AuthenticateResponse>(dbUser);
            loginResult.JwtToken = GenerateJwtToken(dbUser);

            return loginResult;
        }

        public User GetById(int userId) => _dbContext.Users.Find(userId);

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("isAdmin", user.IsAdmin.ToString()));
            var doctorId = GetDoctorIdIfUserIsDoctor(user.Id);
            if (doctorId is not null)
                claims.Add(new Claim("doctorId", doctorId.ToString()));

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtPrivateKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private int? GetDoctorIdIfUserIsDoctor(int userId)
        {
            return _dbContext.Doctors.FirstOrDefault(d => d.UserId == userId)?.Id;
        }
    }
}
