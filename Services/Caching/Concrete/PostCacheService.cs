using BlogSite.Business.Constants;
using Caching.Abstract;
using Caching.Entities;
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
        private IPostRepository _postRepository;
        private readonly IDatabase _cache;

        public PostCacheService(IConnectionMultiplexer redisCon, IDatabase cache, IPostRepository postRepository)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase(0);
            _postRepository = postRepository;
        }

        public async Task<bool> DeleteAsync(string key, Guid id)
        {
            await _cache.HashDeleteAsync(key, id.ToString());
            return true;
        }

        public async Task<List<Post>> GetAsync(string key)
        {
            if (!await _cache.KeyExistsAsync(PostCacheKeys.PostKey))
                return await LoadToCacheFromDbAsync();

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
            await _cache.HashSetAsync(PostCacheKeys.PostKey, entity.Id.ToString(), JsonSerializer.Serialize(entity));
            return true;
        }

        private async Task<List<Post>> LoadToCacheFromDbAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            posts.ForEach(p =>
            {
                _cache.HashSetAsync(PostCacheKeys.PostKey, p.Id.ToString(), JsonSerializer.Serialize(p));

            });
            return posts;
        }
    }
}
