using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginUserVM loginUserVM)
        {
            var userToLogin = await _authService.LoginAsync(loginUserVM);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = await _authService.CreateAccessTokenAsync(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] ViewModels.UserVMs.CreateUserVM createUserVM)
        {

            var registerResult = await _authService.RegisterAsync(createUserVM, createUserVM.Password);
            if (registerResult.Success)
            {
                var result = await _authService.CreateAccessTokenAsync(registerResult.Data);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }          
            return BadRequest(registerResult.Message);
        }
    }
}
