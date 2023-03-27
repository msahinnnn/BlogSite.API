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
        private IDistributedCache _distributedCache;
        private ICommentRepository _commentRepository;

        public TestController(IDistributedCache distributedCache, ICommentRepository commentRepository)
        {
            _distributedCache = distributedCache;
            _commentRepository = commentRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            List<string> listKeys = new List<string>();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:1920"))
            {
                var keys = redis.GetServer("localhost", 1920).Keys();
                listKeys.AddRange(keys.Select(key => (string)key).Where(k=>k.StartsWith("comments")).ToList());
            }
            return Ok(listKeys);
        }

        [HttpGet("second")]
        public async Task<IActionResult> GetSecond()
        {
            //string serializedDatas;
            //List<Comment> datas = new List<Comment>();
            //var jsonDatas = await _distributedCache.GetStringAsync("comments");
            //if (jsonDatas != null)
            //{
            //    serializedDatas = Encoding.UTF8.GetString(jsonDatas);
            //    datas = JsonConvert.DeserializeObject<List<Comment>>(serializedDatas);
            //}
            List<Comment> comments = new List<Comment>();
            List<string> listKeys = new List<string>();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:1920"))
            {
                var keys = redis.GetServer("localhost", 1920).Keys();
                listKeys.AddRange(keys.Select(key => (string)key).ToList());
            }
            foreach (string key in listKeys)
            {
                var userFromCache = await _distributedCache.GetStringAsync(key);
                if (userFromCache != null)
                {
                    // we take User from cache
                    //var serializedUser = Encoding.UTF8.GetString(userFromCache);
                    //var userOut = JsonConvert.DeserializeObject<Comment>(serializedUser);

                    var com = JsonConvert.DeserializeObject<Comment>(userFromCache);
                    comments.Add(com);
                }
            }
            return Ok(comments);
        }
    }
}
