using API.Enums;
using API.ModelsDTO;
using API.Services;
using API.Services.Interfaces;
using DataLibrary.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IDataAccess _dataAccess;
        private readonly IAuthService _authService;

        public AuthController(IDataAccess dataAccess, IConfiguration config)
        {
            _dataAccess = dataAccess;
            _authService = new AuthService(_dataAccess, config);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var loginResult = _authService.Login(login);
            if (!loginResult.Success)
                return Unauthorized();
            else
                return Ok(loginResult);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO register)
        {
            var registerResult = _authService.Register(register);
            return Json(registerResult);
        }

        [HttpGet("validateemail")]
        public IActionResult ValidateEmail(string email)
        {
            var isExist = _authService.CheckIfCustomerExistsByLogin(email);
            return Json(isExist);
        }

        [HttpGet("gettokenvalidation")]
        public IActionResult GetTokenValidation(string token)
        {
            var isValid = _authService.CheckIfTokenIsValid(token);
            return Json(isValid);
        }
    }
}
