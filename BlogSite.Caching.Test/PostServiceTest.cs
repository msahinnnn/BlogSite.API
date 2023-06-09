using Caching.Abstract;
using Caching.Concrete;
using Caching.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Caching.Test
{
    public class PostServiceTest
    {
        public IPostCacheService _postCacheService;
        public IPostService _postService;
        public PostServiceTest()
        {
            _postCacheService = A.Fake<IPostCacheService>();
            _postService = new PostService(_postCacheService);
        }


        [Fact]
        public async void Get_ReturnPosts()
        {
            var posts = A.Fake<List<Post>>();
            A.CallTo(() => _postCacheService.GetAsync()).Returns(posts);
            Assert.NotNull(posts);

            var result = await _postService.GetAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async void SaveOrUpdate_ReturnsTrue()
        {
            var post = A.Fake<Post>();
            A.CallTo(() => _postCacheService.SaveOrUpdateAsync(post)).Returns(true);

            var result = await _postService.SaveOrUpdateAsync(post);
            Assert.True(result);
        }

        [Fact]
        public async void Delete_ReturnsTrue()
        {
            var post = A.Fake<Post>();
            var key = "keyyy";
            A.CallTo(() => _postCacheService.DeleteAsync(post.Id)).Returns(true);

            var result = await _postService.DeleteAsync(post.Id);
            Assert.True(result);
        }
    }
}
