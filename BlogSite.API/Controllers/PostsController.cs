using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Default")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _postService.GetAllAsync();
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetPostByIdAsync([FromQuery] Guid postId)
        {
            var res = await _postService.GetByIdAsync(postId);
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAllPostsByUserIdAsync([FromQuery] Guid userId)
        {
            var res = await _postService.GetPostsByUserIdAsync(userId);
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostVM createPostVM)
        {
            var res = await _postService.CreateAsync(createPostVM);
            if (res.Success == true)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePostVM updatePostVM, [FromQuery] Guid postId)
        {
            var res = await _postService.UpdateAsync(updatePostVM, postId);
            if (res.Success == true)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid postId)
        {
            var res = await _postService.DeleteAsync(postId);
            if (res.Success == true)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }


    }
}
