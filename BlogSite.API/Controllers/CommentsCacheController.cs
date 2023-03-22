using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsCacheController : ControllerBase
    {
        private ICommentCacheService _commentCacheService;
        private readonly CacheService _cacheService;
        public CommentsCacheController(ICommentCacheService commentCacheService, CacheService cacheService)
        {
            _commentCacheService = commentCacheService;
            _cacheService = cacheService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
           var db =  _cacheService.GetDb(0);
           await db.StringSetAsync("adı", "mehmet");
            return Ok();
           //return Ok(await _commentCacheService.GetAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById([FromQuery]Guid id)
        {
            return Ok(await _commentCacheService.GetByIdAsync(id));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Crate([FromBody] CreateCommentVM createCommentVM)
        {
            return Ok(await _commentCacheService.CreateAsync(createCommentVM));
        }





    }
}
