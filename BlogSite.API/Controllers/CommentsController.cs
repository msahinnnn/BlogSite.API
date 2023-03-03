using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            var res = _commentService.GetAllComments();
            if(res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _commentService.GetAllCommentsAsync();
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]")]
        public IActionResult GetCommentById([FromQuery] Guid commentId)
        {
            var res = _commentService.GetCommentById(commentId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetCommentByIdAsync([FromQuery] Guid commentId)
        {
            var res = await _commentService.GetCommentByIdAsync(commentId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]")]
        public IActionResult GetAllCommentByPostId([FromQuery] Guid postId)
        {
            var res = _commentService.GetCommentsByPostId(postId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAllCommentByPostIdAsync([FromQuery] Guid postId)
        {
            var res = await _commentService.GetCommentsByPostIdAsync(postId);
            if (res is not null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] CreateCommentVM createCommentVM)
        {
            var res = _commentService.CreateComment(createCommentVM);
            if(res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentVM createCommentVM)
        {
            var res = await _commentService.CreateCommentAsync(createCommentVM);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public IActionResult Update([FromBody] UpdateCommentVM updateCommentVM, [FromQuery] Guid commentId)
        {
            var res = _commentService.UpdateComment(updateCommentVM, commentId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]Async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCommentVM updateCommentVM, [FromQuery] Guid commentId)
        {
            var res = await _commentService.UpdateCommentAsync(updateCommentVM, commentId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromQuery] Guid commentId)
        {
            var res = _commentService.DeleteComment(commentId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid commentId)
        {
            var res = await _commentService.DeleteCommentAsync(commentId);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
