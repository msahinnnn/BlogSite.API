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
using System.Security.Claims;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostCacheService _postCacheService;
        private IPostService _postService;
        private IPublishEndpoint _publishEndpoint;


        
        public PostsController(IPostService postService, IPublishEndpoint publishEndpoint, IPostCacheService postCacheService)
        {
            _postService = postService;
            _publishEndpoint = publishEndpoint;
            _postCacheService = postCacheService;
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _postCacheService.GetAsync();
            if(res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetPostByIdAsync([FromQuery] Guid postId)
        {
            var res = await _postCacheService.GetByIdAsync(postId);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAllPostsByUserIdAsync([FromQuery] Guid userId)
        {
            var res = await _postService.GetPostsByUserIdAsync(userId);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Admin, User")]
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostVM createPostVM)
        {
            var res = await _postService.CreateAsync(createPostVM);
            if (res != null)
            {
               await _publishEndpoint.Publish(new PostCreatedEvent()
                {
                    Id = res.Id,
                    CreatedDate = res.CreatedDate,
                    Title = res.Title,
                    Content = res.Content,
                    UserId = res.UserId
                });
                return Ok();
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Admin, User")]
        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePostVM updatePostVM, [FromQuery] Guid postId)
        {
            var res = await _postService.UpdateAsync(updatePostVM, postId);
            if (res == true)
            {
                await _publishEndpoint.Publish<PostUpdatedEvent>(new PostUpdatedEvent()
                {
                    Id = postId,
                    Title = updatePostVM.Title,
                    Content = updatePostVM.Content,
                    UserId = updatePostVM.UserId
                }) ;
                return Ok();
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Admin, User")]
        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid postId)
        {
            var res = await _postService.DeleteAsync(postId);
            if (res == true)
            {
                await _publishEndpoint.Publish<PostDeletedEvent>(new PostDeletedEvent()
                {
                    Id = postId,
                });
                return Ok();
            }
            return BadRequest();
        }


    }
}
