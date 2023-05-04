using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedMessages.Models;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;
        private IPublishEndpoint _publishEndpoint;


        public PostsController(IPostService postService, IPublishEndpoint publishEndpoint)
        {
            _postService = postService;
            _publishEndpoint = publishEndpoint;
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

        //[Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostVM createPostVM)
        {
            var res = await _postService.CreateAsync(createPostVM);
            if (res.Success == true)
            {
                await _publishEndpoint.Publish<PostCreatedEvent>(new PostCreatedEvent()
                {
                    Id = res.Data.Id,
                    CreatedDate = res.Data.CreatedDate,
                    Title = res.Data.Title,
                    Content = res.Data.Content,
                    UserId = res.Data.UserId
                });
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

        //[Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePostVM updatePostVM, [FromQuery] Guid postId)
        {
            var res = await _postService.UpdateAsync(updatePostVM, postId);
            if (res.Success == true)
            {
                await _publishEndpoint.Publish<PostUpdatedEvent>(new PostUpdatedEvent()
                {
                    Id = postId,
                    Title = updatePostVM.Title,
                    Content = updatePostVM.Content,
                    UserId = updatePostVM.UserId
                });
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

        //[Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid postId)
        {
            var res = await _postService.DeleteAsync(postId);
            if (res.Success == true)
            {
                await _publishEndpoint.Publish<PostDeletedEvent>(new PostDeletedEvent()
                {
                    Id = postId,
                });
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }


    }
}
