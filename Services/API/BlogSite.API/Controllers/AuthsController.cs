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
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthService _authService;
        public AuthsController(IAuthService authService )
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserVM createUserVM)
        {
            var res = await _authService.RegisterAsync(createUserVM);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserVM loginUserVM)
        {
            var res = await _authService.LoginAsync(loginUserVM);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var res = await _authService.RefreshAsync(tokenDto);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


    }
}
