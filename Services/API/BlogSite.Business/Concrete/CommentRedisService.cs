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
    public class CommentRedisService : ICommentRedisService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private ICommentRepository _commentRepository;
        private readonly IDatabase _cache;


        public CommentRedisService(IConnectionMultiplexer redisCon, IDatabase cache, ICommentRepository commentRepository)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase(0);
            _commentRepository = commentRepository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _cache.HashDeleteAsync(CommentCacheKeys.CommentKey, id.ToString());
            return true;
        }

        public async Task<List<Comment>> GetAsync()
        {
            if (!await _cache.KeyExistsAsync(CommentCacheKeys.CommentKey))
                return await LoadToCacheFromDbAsync();

            var comments = new List<Comment>();

            var cacheComments = await _cache.HashGetAllAsync(CommentCacheKeys.CommentKey);
            foreach (var item in cacheComments.ToList())
            {
                var comment = JsonSerializer.Deserialize<Comment>(item.Value);
                comments.Add(comment);
            }
            return comments;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            if (_cache.KeyExists(CommentCacheKeys.CommentKey))
            {
                var comment = await _cache.HashGetAsync(CommentCacheKeys.CommentKey, id.ToString());
                return comment.HasValue ? JsonSerializer.Deserialize<Comment>(comment) : null;
            }

            var comments = await LoadToCacheFromDbAsync();
            return comments.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> SaveOrUpdateAsync(Comment entity)
        {
            await _cache.HashSetAsync(CommentCacheKeys.CommentKey, entity.Id.ToString(), JsonSerializer.Serialize(entity));
            return true;
        }

        public async Task<List<Comment>> LoadToCacheFromDbAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            comments.ForEach(p =>
            {
                _cache.HashSetAsync(CommentCacheKeys.CommentKey, p.Id.ToString(), JsonSerializer.Serialize(p));

            });
            return comments;
        }
    }
}
