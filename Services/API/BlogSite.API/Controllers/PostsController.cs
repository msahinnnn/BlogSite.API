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
using SharedMessages;

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

        [Authorize(Roles = "Admin, User")]
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostVM createPostVM)
        {
            await _publishEndpoint.Publish(new PostCreatedEvent()
            {
                //Id = res.Data.Id,
                //CreatedDate = res.Data.CreatedDate,
                //Title = res.Data.Title,
                //Content = res.Data.Content,
                //UserId = res.Data.UserId

                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Title = createPostVM.Title,
                Content = createPostVM.Content,
                UserId = Guid.NewGuid()
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
                UserId = updatePostVM.UserId
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
