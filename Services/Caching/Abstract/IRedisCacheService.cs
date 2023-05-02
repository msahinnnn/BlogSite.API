using BlogSite.API.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Core.Services
{
    public interface IRedisCacheService<T>
    {
        Task<List<T>> GetAsync(string key);
        Task<bool> SaveOrUpdateAsync(T entity);
        Task<bool> DeleteAsync(string key, Guid id);
    }
}
