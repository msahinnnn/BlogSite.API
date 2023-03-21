using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsCacheController : ControllerBase
    {
        private ICommentService _commentService;
        private ICacheService _cacheService;
        public CommentsCacheController(ICommentService commentService, ICacheService cacheService)
        {
            _commentService = commentService;
            _cacheService = cacheService;
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> Get()
        //{
        //    ThreadPool.SetMinThreads(10, 10);
        //    var cachedData =  _cacheService.GetData<IEnumerable<Comment>>("comments");
        //    if (cachedData != null && cachedData.Count() > 0)
        //    {
        //        return Ok(cachedData);
        //    }
        //    var res = await _commentService.GetAllCommentsAsync();
        //    cachedData = res.Data;
        //    var expiryTime = DateTimeOffset.Now.AddSeconds(30);
        //    _cacheService.SetData<IEnumerable<Comment>>("comments", cachedData, expiryTime);
        //    return Ok(cachedData);
        //}

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Post([FromBody] CreateCommentVM createCommentVM)
        //{
        //    ThreadPool.SetMinThreads(10, 10);
        //    var res = await _commentService.CreateCommentAsync(createCommentVM);
        //    if (res.Success)
        //    {
        //        var expiryTime = DateTimeOffset.Now.AddSeconds(30);
        //        _cacheService.SetData<Comment>($"comment{res.Data.Id}", res.Data, expiryTime);
        //        return Ok(res.Data);
        //    }
        //    return BadRequest();
        //}

        //[HttpDelete("[action]")]
        //public async Task<IActionResult> Delete([FromQuery] Guid id)
        //{
        //    ThreadPool.SetMinThreads(10, 10);
        //    var check = await _commentService.GetCommentByIdAsync(id);
        //    if (check.Success)
        //    {
        //        await _commentService.DeleteCommentAsync(id);
        //        _cacheService.RemoveData($"comment{id}");
        //        return Ok();
        //    }
        //    return BadRequest();
        //}



        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var cachedData = await _cacheService.GetDataAsync<IEnumerable<Comment>>("comments");
            if (cachedData != null && cachedData.Count() > 0)
            {
                return Ok(cachedData);
            }
            var res = await _commentService.GetAllCommentsAsync();
            cachedData = res.Data;
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            await _cacheService.SetDataAsync<IEnumerable<Comment>>("comments", cachedData, expiryTime);
            return Ok(cachedData);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] CreateCommentVM createCommentVM)
        {
            var res = await _commentService.CreateCommentAsync(createCommentVM);
            if (res.Success)
            {
                var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                await _cacheService.SetDataAsync<Comment>($"comment{res.Data.Id}", res.Data, expiryTime);
                return Ok(res.Data);
            }
            return BadRequest();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var check = await _commentService.GetCommentByIdAsync(id);
            if (check.Success)
            {
                await _commentService.DeleteCommentAsync(id);
                await _cacheService.RemoveDataAsync($"comment{id}");
                return Ok();
            }
            return BadRequest();
        }
    }
}
