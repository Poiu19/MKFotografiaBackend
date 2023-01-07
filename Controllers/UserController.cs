using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKFotografiaBackend.Models.Incoming;
using MKFotografiaBackend.Services;

namespace MKFotografiaBackend.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            return Ok(_userService.Login(dto));
        }
        [HttpPost("register")]
        [Authorize(Roles = "Administrator, Developer")]
        public ActionResult Register([FromBody] RegisterUserDto dto)
        {
            var id = _userService.RegisterUser(dto);
            return Created($"/api/user/{id}", null);
        }
        [HttpPut("reset-admin")]
        public ActionResult ResetAdmin([FromBody] ResetAdminDto dto)
        {
            if (_webHostEnvironment.IsDevelopment() || _webHostEnvironment.IsStaging())
            {
                _userService.ResetAdmin(dto);
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }
}
