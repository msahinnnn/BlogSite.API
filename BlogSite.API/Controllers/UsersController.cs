using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using FluentValidation;
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

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            var res = _userService.GetAllUsers();
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]")]
        public IActionResult GetUserById([FromQuery] Guid userId)
        {
            var res = _userService.GetUserById(userId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }


        [HttpPost("[action]")]
        public IActionResult Create([FromBody] CreateUserVM createUserVM)
        {
            var res = _userService.CreateUser(createUserVM);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody] UpdateUserVM updateUserVM, [FromQuery] Guid userId)
        {
            var res = _userService.UpdateUser(updateUserVM, userId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromQuery] Guid userId)
        {
            var res = _userService.DeleteUser(userId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
