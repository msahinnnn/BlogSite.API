using BlogSite.API.Models;
using BlogSite.DataAccsess.Abstract;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Data;
using System.Data.SqlClient;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private IPostRepository _postRepository;

        public TestsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = _postRepository.GetAllAsync();
            return Ok(posts);
        }
    }
}
