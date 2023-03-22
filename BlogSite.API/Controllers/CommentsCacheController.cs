using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsCacheController : ControllerBase
    {
        private ICommentCacheService _commentCacheService;
        private readonly CacheService _cacheService;
        private IDistributedCache _distributedCache;
        public CommentsCacheController(ICommentCacheService commentCacheService, CacheService cacheService, IDistributedCache distributedCache)
        {
            _commentCacheService = commentCacheService;
            _cacheService = cacheService;
            _distributedCache = distributedCache;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            //var db =  _cacheService.GetDb(0);
            //await db.StringSetAsync("adı", "mehmet");
            //await _distributedCache.SetStringAsync("test","test");
            //return Ok();
           return Ok(await _commentCacheService.GetAsync());
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
