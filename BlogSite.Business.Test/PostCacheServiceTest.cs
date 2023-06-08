using BlogSite.API.Models;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.Business.Constants;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class PostCacheServiceTest : IClassFixture<PostCacheService>
    {
        private IPostRedisService _postRedisService;
        private IPostCacheService _postCacheService;

        public PostCacheServiceTest(IPostRedisService postRedisService, IPostCacheService postCacheService)
        {
            _postRedisService = A.Fake<IPostRedisService>();
            _postCacheService = new PostCacheService(_postRedisService);
        }

        [Fact]
        public async void GetPosts_ReturnsPostsFromCache()
        {
            var x = A.CallTo(() => _postRedisService.GetAsync());
        }
        
    }
}
