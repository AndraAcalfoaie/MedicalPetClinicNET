using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalPetClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<WeatherForecastController> _logger;

        public UserController(IUserService userService, ILogger<WeatherForecastController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public ActionResult<LoginUserDto> Login([FromBody] string email, [FromBody] string password)
        {
            return _userService.Login(email, password);
        }


        [HttpPost("Register")]
        public ActionResult<LoginUserDto> Register([FromBody] RegisterUserDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return _userService.Register(user);
        }
    }
}
