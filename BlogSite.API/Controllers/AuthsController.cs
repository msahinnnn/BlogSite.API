using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Entities.ViewModels.UserVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthService _authService;
        private ILogger<AuthsController> _logger;
        public AuthsController(IAuthService authService, ILogger<AuthsController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserVM createUserVM)
        {
            var res = await _authService.RegisterAsync(createUserVM);
            if (res.Success)
            {
                _logger.LogInformation(res.Message, res);
                return Ok(res.Data);
            }
            _logger.LogError(res.Message, res);
            return BadRequest(res.Message);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserVM loginUserVM)
        {
            var res = await _authService.LoginAsync(loginUserVM);
            if (res.Success)
            {
                _logger.LogInformation(res.Message, res);
                return Ok(res.Data);
            }
            _logger.LogError(res.Message, res);
            return BadRequest(res.Message);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var res = await _authService.RefreshAsync(tokenDto);
            if (res.Success)
            {
                _logger.LogInformation(res.Message, res);
                return Ok(res.Data);
            }
            _logger.LogError(res.Message, res);
            return BadRequest(res.Message);
        }
    }
}
