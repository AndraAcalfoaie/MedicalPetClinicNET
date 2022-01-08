using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters;

namespace MedicalPetClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public ActionResult<AuthenticateResponse> Login([FromBody] LoginModelDto loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return _userService.Login(loginModel);
        }


        [HttpPost("Register")]
        public ActionResult<AuthenticateResponse> Register([FromBody] RegisterUserDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return _userService.Register(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Authorized");
        }
    }
}
