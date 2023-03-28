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


        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _commentService.GetAllAsync();
            if (res.Success == true)
            {
                return Ok(res.Data);
                //return Ok(res);
            }
            return BadRequest(res.Message);
            //return Ok("test");
        }

        [HttpGet("[action]Async")]
        public async Task<IActionResult> GetCommentByIdAsync([FromQuery] Guid commentId)
        {
            var res = await _commentService.GetByIdAsync(commentId);
            if (res.Success == true)
            {
                return Ok(res.Data);
                //return Ok(res);
            }
            return BadRequest(res.Message);
        }

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

        [HttpPost("[action]Async")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentVM createCommentVM)
        {
            var res = await _commentService.CreateAsync(createCommentVM);
            if (res.Success == true)
            {
                // return Ok(res.Message);
                return Ok(res);
            }
            return BadRequest(res.Message);
        }

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

        [HttpDelete("[action]Async")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid commentId)
        {
            var res = await _commentService.DeleteAsync(commentId);
            if (res.Success == true)
            {
                return Ok(res.Message);
                //return Ok(res);
            }
            return BadRequest(res.Message);
        }



        //[HttpGet("[action]")]
        //public IActionResult Get()
        //{
        //    var res = _commentService.GetAllComments();
        //    if (res is not null)
        //    {
        //        return Ok(res);
        //    }
        //    return BadRequest();
        //}


        //[HttpGet("[action]")]
        //public IActionResult GetCommentById([FromQuery] Guid commentId)
        //{
        //    var res = _commentService.GetCommentById(commentId);
        //    if (res is not null)
        //    {
        //        return Ok(res);
        //    }
        //    return BadRequest();
        //}


        //[HttpGet("[action]")]
        //public IActionResult GetAllCommentByPostId([FromQuery] Guid postId)
        //{
        //    var res = _commentService.GetCommentsByPostId(postId);
        //    if (res is not null)
        //    {
        //        return Ok(res);
        //    }
        //    return BadRequest();
        //}


        //[HttpPost("[action]")]
        //public IActionResult Create([FromBody] CreateCommentVM createCommentVM)
        //{
        //    var res = _commentService.CreateComment(createCommentVM);
        //    if(res == true)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}


        //[HttpPut("[action]")]
        //public IActionResult Update([FromBody] UpdateCommentVM updateCommentVM, [FromQuery] Guid commentId)
        //{
        //    var res = _commentService.UpdateComment(updateCommentVM, commentId);
        //    if (res == true)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}


        //[HttpDelete("[action]")]
        //public IActionResult Delete([FromQuery] Guid commentId)
        //{
        //    var res = _commentService.DeleteComment(commentId);
        //    if (res == true)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}


    }
}
