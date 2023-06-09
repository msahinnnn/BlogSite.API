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
    public class PostCacheServiceTest 
    {
        private IPostRedisService _postRedisService;
        private IPostCacheService _postCacheService;

        public PostCacheServiceTest()
        {
            _postRedisService = A.Fake<IPostRedisService>();
            _postCacheService = new PostCacheService(_postRedisService);
        }

        [Fact]
        public async void GetPosts_ReturnsPostsFromCache()
        {
            var posts = A.Fake<List<Post>>();
            var x = A.CallTo(() => _postRedisService.GetAsync()).Returns(posts);

            var result = await _postCacheService.GetAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetPostById_ReturnsPostFromCache()
        {
            var post = A.Fake<Post>();
            A.CallTo(() => _postRedisService.GetByIdAsync(post.Id)).Returns(post);

            var result = await _postCacheService.GetByIdAsync(post.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeletePost_ReturnsTrue()
        {
            var post = A.Fake<Post>();
            var x = A.CallTo(() => _postRedisService.DeleteAsync(post.Id)).Returns(true);

            var result = await _postCacheService.DeleteAsync(post.Id);
            Assert.True(result);
        }

        [Fact]
        public async void SaveOrUpdatePost_ReturnsTrue()
        {
            var post = A.Fake<Post>();
            A.CallTo(() => _postRedisService.SaveOrUpdateAsync(post)).Returns(true);
            
            var result = await _postCacheService.SaveOrUpdateAsync(post);
            Assert.True(result);
        }
    }
}
