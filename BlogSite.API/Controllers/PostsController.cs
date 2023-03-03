using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //private IPostService _postService;
        ////private readonly IValidator<CreatePostVM> _validator;

        //public PostsController(IPostService postService/*, IValidator<CreatePostVM> validator*/)
        //{
        //    _postService = postService;
        //    //_validator = validator;
        //}

        //[HttpGet]
        //public IActionResult Get([FromQuery]Pagination pagination)
        //{
        //    var posts = _postService.GetPosts().Skip((pagination.Page - 1) * pagination.Size).Take(pagination.Size).ToList();
        //    return Ok(posts);
        //}

        //[HttpPost]
        //public IActionResult Post([FromBody]CreatePostVM createPostVM, [FromQuery] Guid userId)
        //{
        //    //var validation = _validator.Validate(createPostVM);
        //    //if (validation.IsValid)
        //    //{
        //        _postService.CreatePost(createPostVM, userId);
        //        return Ok();
        //    //}
        //    //return BadRequest(validation.Errors);
        //}
    }
}
