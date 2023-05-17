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
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _userService.GetAllAsync();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] Guid userId)
        {
            var res = await _userService.GetByIdAsync(userId);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserVM createUserVM)
        {
            var res = await _userService.CreateAsync(createUserVM);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        //[Authorize(Roles = "User")]
        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserVM updateUserVM, [FromQuery] Guid userId)
        {
            var res = await _userService.UpdateAsync(updateUserVM, userId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        //[Authorize(Roles = "User")]
        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid userId)
        {
            var res = await _userService.DeleteAsync(userId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
