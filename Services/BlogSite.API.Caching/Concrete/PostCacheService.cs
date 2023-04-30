using BlogSite.API.Caching.Abstract;
using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
using BlogSite.Business.Constants;
using BlogSite.Core.Entities;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Concrete
{
    public class PostCacheService : IPostCacheService
    {
        private IDatabase _db;
        private ConnectionMultiplexer _redis;
        private IPostRepository _repository;
        public PostCacheService(IDatabase db)
        {
            _redis = ConnectionMultiplexer.Connect("app_redis");
            _db = _redis.GetDatabase(0);
        }

        public async Task<IResult> DeleteAsync(string key, Guid id)
        {
            await _db.HashDeleteAsync(key, id.ToString());
            return new SuccessResult(RedisMessages.ItemDeleted);
        }

        public async Task<IDataResult<List<Post>>> GetAsync(string key)
        {
            if (!await _db.KeyExistsAsync(PostCacheKeys.PostKey))
            {
                var res = await LoadToCacheFromDbAsync();
                return new SuccessDataResult<List<Post>>(res.Data, RedisMessages.ItemsListed);
            }

            var posts = new List<Post>();

            var cachePosts = await _db.HashGetAllAsync(PostCacheKeys.PostKey);
            foreach (var item in cachePosts.ToList())
            {
                var post = JsonSerializer.Deserialize<Post>(item.Value);
                posts.Add(post);

            }
            return new SuccessDataResult<List<Post>>(posts, RedisMessages.ItemsListed);
        }


        public async Task<IDataResult<List<Post>>> LoadToCacheFromDbAsync()
        {
            var posts = await _repository.GetAllAsync();
            posts.ForEach(c =>
            {
                _db.HashSetAsync(PostCacheKeys.PostKey, c.Id.ToString(), JsonSerializer.Serialize(c));
            });
            return new SuccessDataResult<List<Post>>(posts, RedisMessages.ItemAdded);
        }


        public async Task<IResult> SaveOrUpdateAsync(IMessage entity)
        {
            var newPost = await _repository.CreateAsync((Post)entity);
            if (await _db.KeyExistsAsync(PostCacheKeys.PostKey))
            {
                await _db.HashSetAsync(PostCacheKeys.PostKey, entity.Id.ToString(), JsonSerializer.Serialize(newPost));
            }
            return new SuccessResult(RedisMessages.ItemAdded);
        }
    }
}
