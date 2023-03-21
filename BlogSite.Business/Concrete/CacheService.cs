using BlogSite.Business.Abstract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CacheService : ICacheService
    {
        private IDatabase _cacheDb;

        public CacheService( )
        {
            var redis = ConnectionMultiplexer.Connect("127.0.0.1:6379, abortConnect=false, ssl=true, sslprotocols=tls12");
            _cacheDb = redis.GetDatabase();

        }

        public async Task<T> GetDataAsync<T>(string key)
        {
            var res = await _cacheDb.StringGetAsync(key);
            if (!string.IsNullOrEmpty(res))
            {
                return  JsonSerializer.Deserialize<T>(res);
            }
            return default;
               
        }
       
        public async Task<object> RemoveDataAsync(string key)
        {
            var check = await _cacheDb.KeyExistsAsync(key);
            if(check)
            {
                return await _cacheDb.KeyDeleteAsync(key);
            }
            return false;
        }
    
        public async Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return await _cacheDb.StringSetAsync(key, JsonSerializer.Serialize(value), expirtyTime);
        }

        public T GetData<T>(string key)
        {
            var res =  _cacheDb.StringGet(key);
            if (!string.IsNullOrEmpty(res))
            {
                return JsonSerializer.Deserialize<T>(res);
            }
            return default;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return  _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);
        }

        public object RemoveData(string key)
        {
            var check =  _cacheDb.KeyExists(key);
            if (check)
            {
                return  _cacheDb.KeyDelete(key);
            }
            return false;
        }
    }
}
