
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
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
        //private ICommentRepository _commentRepository;
        //private ICommentService _commentService;
        

        //public CommentsController(ICommentService commentService,  ICommentRepository commentRepository)
        //{
        //    _commentService = commentService;
        //    _commentRepository = commentRepository;
        //}

        //[HttpGet("[action]")]
        //public IActionResult Get()
        //{
        //    var comments = _commentService.GetComments();
        //    return Ok(comments);
        //}

        //[HttpGet("[action]/{postId}")]
        //public IActionResult GetCommentsByPostId(Guid postId)
        //{
        //    var comments = _commentRepository.GetCommentsByPostId(postId);
        //    return Ok(comments);
        //}

        //[HttpGet("[action]/{commentId}")]
        //public IActionResult GetCommentById(Guid commentId)
        //{
        //    var comments = _commentRepository.GetCommentById(commentId);
        //    return Ok(comments);
        //}

        //[HttpPost("[action]")]
        //public IActionResult Post([FromBody] CreateCommentVM createCommentVM, [FromQuery] Guid postId)
        //{
        //    return Ok();
        //}

        //[HttpDelete("[action]")]
        //public IActionResult Delete([FromQuery] Guid commentId)
        //{
        //    var res = _commentRepository.DeleteComment(commentId);
        //    return Ok(res);
        //}

        //[HttpPut("[action]")]
        //public IActionResult Update([FromBody] UpdateCommentVM updateCommentVM, [FromQuery] Guid commentId)
        //{
        //    var res = _commentRepository.UpdateComment(updateCommentVM, commentId);
        //    return Ok(res);
        //}
    }
}
