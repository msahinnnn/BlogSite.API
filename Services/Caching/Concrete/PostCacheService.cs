using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
using BlogSite.Business.Constants;
using BlogSite.Core.DataAccess;
using BlogSite.Core.Utilities.Results;
using Caching.Abstract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Caching.Concrete
{
    public class PostCacheService : IPostCacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;

        public PostCacheService(IConnectionMultiplexer redisCon, IDatabase cache)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase(0);
        }

        public async Task<bool> DeleteAsync(string key, Guid id)
        {
            await _cache.HashDeleteAsync(key, id.ToString());
            return true;
        }

        public async Task<List<Post>> GetAsync(string key)
        {
            var posts = new List<Post>();

            var cachePosts = await _cache.HashGetAllAsync(PostCacheKeys.PostKey);
            foreach (var item in cachePosts.ToList())
            {
                var post = JsonSerializer.Deserialize<Post>(item.Value);
                posts.Add(post);
            }
            return posts;
        }


        public async Task<bool> SaveOrUpdateAsync(Post entity)
        {
            //if (await _cache.KeyExistsAsync(PostCacheKeys.PostKey))
            //{
                await _cache.HashSetAsync(PostCacheKeys.PostKey, entity.Id.ToString(), JsonSerializer.Serialize(entity));
            //}
            return true;
        }
    }
}
