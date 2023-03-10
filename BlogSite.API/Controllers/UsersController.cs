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
       

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _userService.GetAllUsersAsync();
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }


        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] Guid userId)
        {
            var res = await _userService.GetUserByIdAsync(userId);
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }


        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserVM createUserVM)
        {
            var res = await _userService.CreateUserAsync(createUserVM);
            if (res.Success == true)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }


        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserVM updateUserVM, [FromQuery] Guid userId)
        {
            var res = await _userService.UpdateUserAsync(updateUserVM, userId);
            if (res.Success == true)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }


        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid userId)
        {
            var res = await _userService.DeleteUserAsync(userId);
            if (res.Success == true)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }


        //[HttpGet("[action]")]
        //public IActionResult Get()
        //{
        //    var res = _userService.GetAllUsers();
        //    if (res is not null)
        //    {
        //        return Ok(res);
        //    }
        //    return BadRequest();
        //}


        //[HttpGet("[action]")]
        //public IActionResult GetUserById([FromQuery] Guid userId)
        //{
        //    var res = _userService.GetUserById(userId);
        //    if (res is not null)
        //    {
        //        return Ok(res);
        //    }
        //    return BadRequest();
        //}


        //[HttpPost("[action]")]
        //public IActionResult Create([FromBody] CreateUserVM createUserVM)
        //{
        //    var res = _userService.CreateUser(createUserVM);
        //    if (res == true)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}


        //[HttpPut("[action]")]
        //public IActionResult Update([FromBody] UpdateUserVM updateUserVM, [FromQuery] Guid userId)
        //{
        //    var res = _userService.UpdateUser(updateUserVM, userId);
        //    if (res == true)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}


        //[HttpDelete("[action]")]
        //public IActionResult Delete([FromQuery] Guid userId)
        //{
        //    var res = _userService.DeleteUser(userId);
        //    if (res == true)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}


    }
}
