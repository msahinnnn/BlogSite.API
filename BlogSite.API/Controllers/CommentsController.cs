using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Default")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService _commentService;
        private ILogger<CommentsController> _logger;
        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
        {
            _commentService = commentService;
            _logger = logger;
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
        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentVM createCommentVM)
        {
            var res = await _commentService.CreateAsync(createCommentVM);
            if (res.Success == true)
            {
                return Ok(res);
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
                return Ok(res.Message);
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
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

    }
}
