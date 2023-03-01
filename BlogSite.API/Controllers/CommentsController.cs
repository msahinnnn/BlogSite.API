using BlogSite.API.Services.Abstract;
using BlogSite.API.Services.Concrete;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
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
        private readonly IValidator<CreateCommentVM> _validator;

        public CommentsController(ICommentService commentService, IValidator<CreateCommentVM> validator)
        {
            _commentService = commentService;
            _validator = validator;
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
            var validation = _validator.Validate(createCommentVM);
            if (validation.IsValid)
            {
                _commentService.CreateComment(createCommentVM, postId);
                return Ok();
            }
            return BadRequest(validation.Errors);
        }
    }
}
