using BlogSite.API.Models;
using BlogSite.API.Services.Abstract;
using BlogSite.API.Services.Concrete;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]Pagination pagination)
        {
            var posts = _postService.GetPosts().Skip((pagination.Page - 1) * pagination.Size).Take(pagination.Size).ToList();
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreatePostVM createPostVM, [FromQuery] Guid userId)
        {
            _postService.CreatePost(createPostVM, userId);
            return Ok();
        }
    }
}
