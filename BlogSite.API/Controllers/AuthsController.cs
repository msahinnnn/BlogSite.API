using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Concrete;
using BlogSite.Core.Utilities.Results;
using BlogSite.Entities.ViewModels.UserVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Default")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthService _authService;
        private ILogger<AuthsController> _logger;
        private ITokenHandler _tokenHandler;
        public AuthsController(IAuthService authService, ILogger<AuthsController> logger, ITokenHandler tokenHandler)
        {
            _authService = authService;
            _logger = logger;
            _tokenHandler = tokenHandler;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetClaimTypes(string accessToken)
        {
            ClaimsPrincipal principal = _tokenHandler.GetPrincipalFromExpiredToken(accessToken);
            var res = principal?.FindAll(ClaimTypes.NameIdentifier)?.Select(x => x.Value).ToList();
            return Ok(res);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetId()
        {
            var res = _authService.GetCurrentUserId();
            return Ok(res);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetEmail()
        {
            var res = _authService.GetCurrentUserMail();
            return Ok(res);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetRole()
        {
            var res = _authService.GetCurrentUserRole();
            return Ok(res);
        }
    }
}
