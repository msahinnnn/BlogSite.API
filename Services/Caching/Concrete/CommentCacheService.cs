using BlogSite.API.Caching.Abstract;
using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
using BlogSite.Business.Constants;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Concrete
{
    public class CommentCacheService  : ICommentCacheService
    {
        private IDatabase _db;
        private ConnectionMultiplexer _redis;
        private ICommentRepository _commentRepository;
        public CommentCacheService(IDatabase db)
        {
            //_redis = ConnectionMultiplexer.Connect("app_redis");
            _redis = ConnectionMultiplexer.Connect("localhost:1920");
            _db = _redis.GetDatabase(0);
        }

        public async Task<IResult> DeleteAsync(string key, Guid id)
        {
            await _db.HashDeleteAsync(key, id.ToString());
            return new SuccessResult(RedisMessages.ItemDeleted);
        }

        public async Task<IDataResult<List<Comment>>> GetAsync(string key)
        {
            if (!await _db.KeyExistsAsync(CommentCacheKeys.CommentKey))
            {
                var res = await LoadToCacheFromDbAsync();
                return new SuccessDataResult<List<Comment>>(res.Data, RedisMessages.ItemsListed);
            }
                
            var comments = new List<Comment>();

            var cacheComments = await _db.HashGetAllAsync(CommentCacheKeys.CommentKey);
            foreach (var item in cacheComments.ToList())
            {
                var product = JsonSerializer.Deserialize<Comment>(item.Value);
                comments.Add(product);

            }
            return new SuccessDataResult<List<Comment>>(comments, RedisMessages.ItemsListed);

        }

        public async Task<IDataResult<List<Comment>>> LoadToCacheFromDbAsync( )
        {
            var comments = await _commentRepository.GetAllAsync();
            comments.ForEach(c =>
            {
                 _db.HashSetAsync(CommentCacheKeys.CommentKey, c.Id.ToString(), JsonSerializer.Serialize(c));
            });
            return new SuccessDataResult<List<Comment>>(comments, RedisMessages.ItemAdded);
        }

        public async Task<IResult> SaveOrUpdateAsync(IMessage entity)
        {
            var newComment = await _commentRepository.CreateAsync((Comment) entity);
            if (await _db.KeyExistsAsync(CommentCacheKeys.CommentKey))
            {
                await _db.HashSetAsync(CommentCacheKeys.CommentKey, entity.Id.ToString(), JsonSerializer.Serialize(newComment));
            }
            return new SuccessResult(RedisMessages.ItemAdded);
        }
    }

}
