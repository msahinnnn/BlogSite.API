using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Messages.Events;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService _commentService;
        private ICommentCacheService _commentCacheService;
        private IAuthService _authService;
        private ILogger<CommentsController> _logger;
        private IPublishEndpoint _publishEndpoint;

        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger, IPublishEndpoint publishEndpoint, IAuthService authService, ICommentCacheService commentCacheService)
        {
            _commentService = commentService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _authService = authService;
            _commentCacheService = commentCacheService;
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _commentCacheService.GetAsync();
            return Ok(res);
            
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetCommentByIdAsync([FromQuery] Guid commentId)
        {
            var res = await _commentCacheService.GetByIdAsync(commentId);
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAllCommentsByPostIdAsync([FromQuery] Guid postId)
        {
            var res = await _commentService.GetCommentsByPostIdAsync(postId);
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentVM createCommentVM)
        {
            var res = await _commentService.CreateAsync(createCommentVM);
            if (res.Success == true)
            {
                await _publishEndpoint.Publish<CommentCreatedEvent>(new CommentCreatedEvent()
                {
                    Id = Guid.NewGuid(),
                    Content = createCommentVM.Content,
                    CreateTime = DateTime.UtcNow,
                    UserId = Guid.Parse(_authService.GetCurrentUserId()),
                    PostId = createCommentVM.PostId,

                });
                return Ok(res.Message);
            }
            return BadRequest(res.Message);


        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCommentVM updateCommentVM, [FromQuery] Guid commentId)
        {
            var res = await _commentService.UpdateAsync(updateCommentVM, commentId);
            if (res.Success == true)
            {
                await _publishEndpoint.Publish<CommentUpdatedEvent>(new CommentUpdatedEvent()
                {
                    Id = commentId,
                    Content = updateCommentVM.Content,
                    UserId = Guid.Parse(_authService.GetCurrentUserId()),
                    PostId = updateCommentVM.PostId,

                });
                return Ok();
            }
            return BadRequest(res.Message);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid commentId)
        {
            var res = await _commentService.DeleteAsync(commentId);
            if (res.Success == true)
            {
                await _publishEndpoint.Publish<CommentDeletedEvent>(new CommentDeletedEvent()
                {
                    Id = commentId,
                });
                return Ok();
            }
            return BadRequest(res.Message);
        }

    }
}
