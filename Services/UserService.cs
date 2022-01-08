using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Services.Exceptions;
using Services.Models.User;
using System.Linq;

namespace Services
{
    public interface IUserService
    {
        LoginUserDto Register(RegisterUserDto user);
        LoginUserDto Login(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public LoginUserDto Login(string email, string password)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (dbUser is null)
                throw new ValidationException("Invalid email or password");

            return _mapper.Map<LoginUserDto>(dbUser);
        }

        public LoginUserDto Register(RegisterUserDto user)
        {
            if (_dbContext.Users.Any(u => u.Email == user.Email))
                throw new ValidationException("Email already used");

            var dbUser = _mapper.Map<User>(user);
            _dbContext.Users.Add(dbUser);
            _dbContext.SaveChanges();

            return _mapper.Map<LoginUserDto>(dbUser);
        }
    }
}
