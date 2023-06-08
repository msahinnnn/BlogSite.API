using BlogSite.API.Models;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.Business.Constants;
using BlogSite.DataAccsess.Abstract;
using FakeItEasy;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class PostCacheServiceTest
    {
        private  Mock<IConnectionMultiplexer> _redisCon;
        private IPostRepository _postRepository;
        private  Mock<IDatabase> _cache;
        private IPostCacheService _postCacheService;
        //private IConnectionMultiplexer _redisCon;
        //private IDatabase _cache;
        public PostCacheServiceTest()
        {
            _postRepository = A.Fake<IPostRepository>();
            //_redisCon = A.Fake<IConnectionMultiplexer>();
            //_cache = A.Fake<IDatabase>();
            _redisCon = new Mock<IConnectionMultiplexer>();
            _cache = new Mock<IDatabase>();
            //_postCacheService = new PostCacheService(_redisCon.Object, _cache.Object, _postRepository);
        }

        public async void GetAsync()
        {
            //if (!await _cache.KeyExistsAsync(PostCacheKeys.PostKey))
            //    return await LoadToCacheFromDbAsync();

            //var posts = new List<Post>();

            //var cachePosts = await _cache.HashGetAllAsync(PostCacheKeys.PostKey);
            //foreach (var item in cachePosts.ToList())
            //{
            //    var post = JsonSerializer.Deserialize<Post>(item.Value);
            //    posts.Add(post);
            //}
            //return posts;

            //_redisCon.Setup(_ => _.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_cache.Object);
            //var posts = A.Fake<List<Post>>();
            //_redisCon.
            //var x = _cache.Setup(x => x.HashGetAllAsync((It.IsAny<string>())));

        }

        public async void GetByIdAsync()
        {

        }

        public async void SaveOrUpdateAsync()
        {

        }

        public async void DeleteAsync()
        {

        }

        public async void LoadToCacheFromDbAsync()
        {

        }


    }
}
