using BlogSite.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IRedisService
    {
        Task<IDataResult<List<T>>> GetAllCacheAsync<T>(string key, List<T>? dataList);
        Task<IDataResult<T>> GetByIdCacheAsync<T>(string key, Guid id);
        Task<IResult> CreateCacheAsync<T>(string key, T data);
        Task<IResult> DeleteCacheAsync(string key, Guid id);
    }
}
