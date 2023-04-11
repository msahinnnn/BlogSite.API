using BlogSite.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {

        [HttpGet("isauthenticated")]
        public async Task<IActionResult> GetAuthenticated()
        {
            var id = HttpContext.User.Identity.IsAuthenticated;
            return Ok(id);
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetCurrentUserName()
        {
            var id = HttpContext.User.Identity.Name;
            return Ok(id);
        }

        [HttpGet("identities")]
        public async Task<IActionResult> GetCurrentUserIdentities()
        {
            var id = HttpContext.User.Identities;
            return Ok(id);
        }

        [HttpGet("claimroles")]
        public async Task<IActionResult> GetCurrentClaimRoles()
        {
            var id = HttpContext.User.ClaimRoles();
            return Ok(id);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetCurrentClaims()
        {
            var id = HttpContext.User.Claims;
            return Ok(id);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCurrentId()
        {
            var id = HttpContext.User.FindFirstValue("Id");
            return Ok(id);
        }
    }
}
