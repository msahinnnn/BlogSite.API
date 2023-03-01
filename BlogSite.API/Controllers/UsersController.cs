using BlogSite.API.Services.Abstract;
using BlogSite.API.ViewModels.UserVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserVM createUserVM)
        {
            _userService.CreateUser(createUserVM);
            return Ok();
        }
    }
}
