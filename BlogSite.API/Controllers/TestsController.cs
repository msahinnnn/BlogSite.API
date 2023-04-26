using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;
        public TestsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var redis = ConnectionMultiplexer.Connect("app_redis");
            var db = redis.GetDatabase(0);
            var x = db.StringSetAsync("mehmet","sahin");
            return Ok(x);
        }
    }
}
