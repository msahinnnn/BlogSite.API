using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Messages.Events;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostCacheService _postCacheService;
        private IPostService _postService;
        private IPublishEndpoint _publishEndpoint;
        private IAuthService _authService;


        public PostsController(IPostService postService, IPublishEndpoint publishEndpoint, IAuthService authService, IPostCacheService postCacheService)
        {
            _postService = postService;
            _publishEndpoint = publishEndpoint;
            _authService = authService;
            _postCacheService = postCacheService;
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _postCacheService.GetAsync();
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetPostByIdAsync([FromQuery] Guid postId)
        {
            var res = await _postCacheService.GetByIdAsync(postId);
            return Ok(res);
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
            await _publishEndpoint.Publish(new PostCreatedEvent()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Title = createPostVM.Title,
                Content = createPostVM.Content,
                UserId = Guid.Parse(_authService.GetCurrentUserId())
            });
            return Ok();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePostVM updatePostVM, [FromQuery] Guid postId)
        {
            await _publishEndpoint.Publish<PostUpdatedEvent>(new PostUpdatedEvent()
            {
                Id = postId,
                Title = updatePostVM.Title,
                Content = updatePostVM.Content,
                UserId = Guid.Parse(_authService.GetCurrentUserId())
            });
            return Ok();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid postId)
        {
            await _publishEndpoint.Publish<PostDeletedEvent>(new PostDeletedEvent()
            {
                Id = postId,
            });
            return Ok();
        }


    }
}
