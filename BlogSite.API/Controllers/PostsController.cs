using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            var res = _postService.GetAllPosts();
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = _postService.GetAllPostsAsync();
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]")]
        public IActionResult GetPostById([FromQuery] Guid postId)
        {
            var res = _postService.GetPostById(postId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetPostByIdAsync([FromQuery] Guid postId)
        {
            var res = await _postService.GetPostByIdAsync(postId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]")]
        public IActionResult GetAllPostsByUserId([FromQuery] Guid userId)
        {
            var res = _postService.GetPostsByUserId(userId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAllPostsByUserIdAsync([FromQuery] Guid userId)
        {
            var res = await _postService.GetPostsByUserIdAsync(userId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] CreatePostVM createPostVM)
        {
            var res = _postService.CreatePost(createPostVM);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostVM createPostVM)
        {
            var res = await _postService.CreatePostAsync(createPostVM);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody] UpdatePostVM updatePostVM, [FromQuery] Guid postId)
        {
            var res = _postService.UpdatePost(updatePostVM, postId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePostVM updatePostVM, [FromQuery] Guid postId)
        {
            var res = await _postService.UpdatePostAsync(updatePostVM, postId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromQuery] Guid postId)
        {
            var res = _postService.DeletePost(postId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid postId)
        {
            var res = await _postService.DeletePostAsync(postId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
