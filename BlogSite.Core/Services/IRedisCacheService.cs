using BlogSite.API.Shared.Messages;
using BlogSite.Core.Entities;
using BlogSite.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Core.Services
{
    public interface IRedisCacheService<T> where T : class, IBaseEntity, new()
    {
        Task<IDataResult<List<T>>> GetAsync( string key);
        Task<IResult> SaveOrUpdateAsync(IMessage entity);
        Task<IResult> DeleteAsync(string key, Guid id);
        Task<IDataResult<List<T>>> LoadToCacheFromDbAsync();
    }
}
