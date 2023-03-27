using BlogSite.API.Models;
using BlogSite.DataAccsess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
    //    private IDistributedCache _distributedCache;
    //    private ICommentRepository _commentRepository;
    //    private IRService _rService;

    //    public TestController(IDistributedCache distributedCache, ICommentRepository commentRepository, IRService rService)
    //    {
    //        _distributedCache = distributedCache;
    //        _commentRepository = commentRepository;
    //        _rService = rService;
    //    }

    //    [HttpGet("[action]")]
    //    public async Task<IActionResult> Get()
    //    {
    //        List<string> listKeys = new List<string>();
    //        using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:1920"))
    //        {
    //            var keys = redis.GetServer("localhost", 1920).Keys();
    //            listKeys.AddRange(keys.Select(key => (string)key).Where(k=>k.StartsWith("comments")).ToList());
    //        }
    //        return Ok(listKeys);
    //    }

    //    [HttpGet("second")]
    //    public async Task<IActionResult> GetSecond()
    //    {
    //        //if (_rService.Any("comments"))
    //        //{
    //            var comments = _rService.Get<List<Comment>>("comments");
    //            return Ok(comments);
    //        //}
    //        //return BadRequest();
    //    }

    }
}
