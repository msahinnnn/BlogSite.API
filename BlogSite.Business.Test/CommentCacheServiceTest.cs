using BlogSite.API.Models;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class CommentCacheServiceTest
    {
        public ICommentRedisService _redisService;
        public ICommentCacheService _commentCacheService;

        public CommentCacheServiceTest()
        {
            _redisService = A.Fake<ICommentRedisService>();
            _commentCacheService = new CommentCacheService(_redisService);
        }

        [Fact]
        public async void GetComments_ReturnsCommentsFromCache()
        {
            var comments = A.Fake<List<Comment>>();
            var x = A.CallTo(() => _redisService.GetAsync()).Returns(comments);

            var result = await _commentCacheService.GetAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetCommentsById_ReturnsCommentFromCache()
        {
            var comment = A.Fake<Comment>();
            A.CallTo(() => _redisService.GetByIdAsync(comment.Id)).Returns(comment);

            var result = await _commentCacheService.GetByIdAsync(comment.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteComments_ReturnsTrue()
        {
            var comment = A.Fake<Comment>();
            var x = A.CallTo(() => _redisService.DeleteAsync(comment.Id)).Returns(true);

            var result = await _commentCacheService.DeleteAsync(comment.Id);
            Assert.True(result);
        }

        [Fact]
        public async void SaveOrUpdateComment_ReturnsTrue()
        {
            var comment = A.Fake<Comment>();
            A.CallTo(() => _redisService.SaveOrUpdateAsync(comment)).Returns(true);

            var result = await _commentCacheService.SaveOrUpdateAsync(comment);
            Assert.True(result);
        }
    }
}
