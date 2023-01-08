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
        private readonly ILogger<UserController> _logger;
        private readonly IUserContextService _userContextService;
        public UserController(IUserService userService, IWebHostEnvironment webHostEnvironment, ILogger<UserController> logger, IUserContextService userContextService)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _userContextService = userContextService;
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            return Ok(_userService.Login(dto));
        }
        [HttpPost("register")]
        [Authorize(Roles = "Administrator,Developer")]
        public ActionResult Register([FromBody] RegisterUserDto dto)
        {
            var log = $"Użytkownik {_userContextService.GetUserName}, userID {_userContextService.GetUserId} tworzy nowego użytkownika - email {dto.Email}, nazwa {dto.Name} {dto.LastName}, id roli {dto.RoleId}";
            _logger.LogTrace(log);
            _logger.LogWarning(log);
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
