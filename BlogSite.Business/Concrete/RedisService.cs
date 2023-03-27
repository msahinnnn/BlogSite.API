using BlogSite.API.Models;
using BlogSite.Business.Abstract;
using BlogSite.Core.Utilities.Results;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class RedisService : IRedisService
    {
        private IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<IDataResult<List<T>>> GetAllCacheAsync<T>(string key, List<T>? dataList)
        {
            string serializedDatas;
            List<T> datas = new List<T>();
            var jsonDatas = await _distributedCache.GetAsync(key);
            if (jsonDatas != null)
            {
                serializedDatas = Encoding.UTF8.GetString(jsonDatas);
                datas = JsonConvert.DeserializeObject<List<T>>(serializedDatas);
            }
            else
            {
                datas = dataList;
                serializedDatas = JsonConvert.SerializeObject(datas);
                jsonDatas = Encoding.UTF8.GetBytes(serializedDatas);
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddDays(7));
                await _distributedCache.SetAsync(key, jsonDatas, options);
            }
            return new DataResult<List<T>>(datas, true, "Cached datas...");

        }


        public async Task<IDataResult<T>> GetByIdCacheAsync<T>(string key, Guid id)
        {
            var res = await _distributedCache.GetStringAsync($"{key}-{id.ToString()}");
            T data = JsonConvert.DeserializeObject<T>(res);
            if(data != null)
            {
                return new DataResult<T>(data, true, "Cached data by Id...");
            }
            return new DataResult<T>(data, false, $"Id : {id} not exists...");
        }

        public async Task<IResult> CreateCacheAsync<T>(string key, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddDays(7));
            await _distributedCache.SetStringAsync(key, jsonData, options);
            return new Result(true, $"Item added to cache...");
        }

        public async Task<IResult> DeleteCacheAsync(string key, Guid id)
        {
            var check = await _distributedCache.GetStringAsync(key);
            if(check != null)
            {
                await _distributedCache.RemoveAsync($"{key}-{id.ToString()}");
                return new Result(true, $"Item deleted from cache...");
            }
            return new Result(false, $"Id : {id} not exists...");
            
        }



        
    }
}
