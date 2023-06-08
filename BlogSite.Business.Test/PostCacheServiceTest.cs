using BlogSite.API.Models;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.Business.Constants;
using BlogSite.Core.Services;
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
        private IPostCacheService _postCacheService;
        private ICacheService _cacheService;
        public PostCacheServiceTest(IPostCacheService postCacheService, ICacheService cacheService)
        {
            _cacheService = A.Fake<ICacheService>();
            _postCacheService = new PostCacheService(_cacheService);
        }


        [Fact]
        public async void GetAsync()
        {
            var cachePosts = A.CallTo(() => _cacheService.GetAsync(A<string>._));
            Assert.NotNull(cachePosts);
        }

        [Fact]
        public async void GetByIdAsync()
        {

        }

        [Fact]
        public async void SaveOrUpdateAsync()
        {

        }

        [Fact]
        public async void DeleteAsync()
        {

        }

        [Fact]
        public async void LoadToCacheFromDbAsync()
        {

        }


    }
}
