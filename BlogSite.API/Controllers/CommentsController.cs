using BlogSite.API.Services.Abstract;
using BlogSite.API.Services.Concrete;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
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

        [HttpGet]
        public IActionResult Get()
        {
            var comments = _commentService.GetComments();
            return Ok(comments);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentVM createCommentVM, [FromQuery] Guid postId)
        {
            _commentService.CreateComment(createCommentVM, postId);
            return Ok();
        }
    }
}
