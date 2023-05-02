using BlogSite.API.Shared.Messages;
using BlogSite.Business.Constants;
using BlogSite.Core.Services;
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
    public class CommentCacheService : ICommentCacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private ICommentRepository _commentRepository;
        private readonly IDatabase _cache;

        public CommentCacheService(IConnectionMultiplexer redisCon, IDatabase cache, ICommentRepository commentRepository)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase(0);
            _commentRepository = commentRepository;
        }

        public async Task<bool> DeleteAsync(string key, Guid id)
        {
            await _cache.HashDeleteAsync(key, id.ToString());
            return true;
        }

        public async Task<List<Comment>> GetAsync(string key)
        {
            if (!await _cache.KeyExistsAsync(PostCacheKeys.PostKey))
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

        public async Task<bool> SaveOrUpdateAsync(Comment entity)
        {
            await _cache.HashSetAsync(CommentCacheKeys.CommentKey, entity.Id.ToString(), JsonSerializer.Serialize(entity));
            return true;
        }

        private async Task<List<Comment>> LoadToCacheFromDbAsync()
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
