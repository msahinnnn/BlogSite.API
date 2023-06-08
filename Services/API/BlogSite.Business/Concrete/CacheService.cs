using BlogSite.Core.DataAccess;
using BlogSite.Core.Entities;
using BlogSite.Core.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;

        public CacheService(IConnectionMultiplexer redisCon, IDatabase cache)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase(0);
        }


        public async Task<List<IBaseEntity>> GetAsync(string key)
        {
            //if (!await _cache.KeyExistsAsync(CommentCacheKeys.CommentKey))
            //    return await LoadToCacheFromDbAsync();

            var entities = new List<IBaseEntity>();

            var cacheEntities = await _cache.HashGetAllAsync(key);
            foreach (var item in cacheEntities.ToList())
            {
                var entity = JsonSerializer.Deserialize<IBaseEntity>(item.Value);
                entities.Add(entity);
            }
            return entities;
        }

        public async Task<IBaseEntity> GetByIdAsync(Guid id, string key)
        {
            if (_cache.KeyExists(key))
            {
                var entity = await _cache.HashGetAsync(key, id.ToString());
                return entity.HasValue ? JsonSerializer.Deserialize<IBaseEntity>(entity) : null;
            }
            return null;
            //var comments = await LoadToCacheFromDbAsync();
            //return comments.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> DeleteAsync(Guid id, string key)
        {
            await _cache.HashDeleteAsync(key, id.ToString());
            return true;
        }

        public async Task<bool> SaveOrUpdateAsync(IBaseEntity entity, string key)
        {
            await _cache.HashSetAsync(key, entity.Id.ToString(), JsonSerializer.Serialize(entity));
            return true;
        }

        public async Task<List<T>> LoadToCacheFromDbAsync<T>(List<T> entities, string key)
        {
            entities.ForEach(p =>
            {
                _cache.HashSetAsync(key, p.Id.ToString(), JsonSerializer.Serialize(p));

            });
            return entities;

        }

    }
}
