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
    public class CommentServiceTest
    {
        public ICommentCacheService _commentCacheService;
        public ICommentService _commentService;
        public CommentServiceTest()
        {
            _commentCacheService = A.Fake<ICommentCacheService>();
            _commentService = new CommentService(_commentCacheService);
        }

        [Fact]
        public async void Get_ReturnPosts()
        {
            var comments = A.Fake<List<Comment>>();
            A.CallTo(() => _commentCacheService.GetAsync()).Returns(comments);
            Assert.NotNull(comments);

            var result = await _commentService.GetAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async void SaveOrUpdate_ReturnsTrue()
        {
            var comment = A.Fake<Comment>();
            A.CallTo(() => _commentCacheService.SaveOrUpdateAsync(comment)).Returns(true);

            var result = await _commentService.SaveOrUpdateAsync(comment);
            Assert.True(result);
        }

        [Fact]
        public async void Delete_ReturnsTrue()
        {
            var comment = A.Fake<Comment>();
            var key = "keyyy";
            A.CallTo(() => _commentCacheService.DeleteAsync(comment.Id)).Returns(true);

            var result = await _commentService.DeleteAsync(comment.Id);
            Assert.True(result);
        }
    }
}
