using BlogSite.API.Models;
using BlogSite.Business.Abstract;
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
        private IAuthService _authService;

        public TestsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var x = _authService.GetCurrentUserId();
            return Ok(x);
        }
    }
}
