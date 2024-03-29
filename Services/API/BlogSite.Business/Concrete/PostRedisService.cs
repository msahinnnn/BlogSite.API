﻿using BlogSite.API.Models;
using BlogSite.Business.Abstract;
using BlogSite.Business.Constants;
using BlogSite.DataAccsess.Abstract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class PostRedisService : IPostRedisService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private IPostRepository _postRepository;
        private readonly IDatabase _cache;

        public PostRedisService(IConnectionMultiplexer redisCon, IDatabase cache, IPostRepository postRepository)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase(0);
            _postRepository = postRepository;
        }


        public async Task<List<Post>> GetAsync()
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

        public async Task<Post> GetByIdAsync(Guid id)
        {
            if (_cache.KeyExists(PostCacheKeys.PostKey))
            {
                var post = await _cache.HashGetAsync(PostCacheKeys.PostKey, id.ToString());
                return post.HasValue ? JsonSerializer.Deserialize<Post>(post) : null;
            }

            var posts = await LoadToCacheFromDbAsync();
            return posts.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _cache.HashDeleteAsync(PostCacheKeys.PostKey, id.ToString());
            return true;
        }

        public async Task<bool> SaveOrUpdateAsync(Post entity)
        {
            await _cache.HashSetAsync(PostCacheKeys.PostKey, entity.Id.ToString(), JsonSerializer.Serialize(entity));
            return true;
        }


        public async Task<List<Post>> LoadToCacheFromDbAsync()
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
