using BlogSite.API.Messages;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
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
        private ILogger<CommentsController> _logger;
        private IPublishEndpoint _publishEndpoint;

        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger, IPublishEndpoint publishEndpoint)
        {
            _commentService = commentService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _commentService.GetAllAsync();
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

        [AllowAnonymous]
        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetCommentByIdAsync([FromQuery] Guid commentId)
        {
            var res = await _commentService.GetByIdAsync(commentId);
            if (res.Success == true)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
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
        [AllowAnonymous]
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentVM createCommentVM)
        {
            var res = await _commentService.CreateAsync(createCommentVM);
            if (res.Success == true)
            {
                await _publishEndpoint.Publish<CommentCreatedEvent>(new CommentCreatedEvent()
                {
                    Id = res.Data.Id,
                    Content = res.Data.Content,
                    CreateTime = res.Data.CreateTime,
                    UserId = res.Data.UserId,
                    PostId = res.Data.PostId,

                });
                return Ok(res);
            }
            return BadRequest(res.Message);
        }

        [Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
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
                    PostId = updateCommentVM.PostId

                });
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

        [Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
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
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

    }
}
