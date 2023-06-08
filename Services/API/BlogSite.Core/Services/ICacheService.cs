using BlogSite.Core.DataAccess;
using BlogSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Core.Services
{
    public interface ICacheService
    {
        Task<List<IBaseEntity>> GetAsync(string key);
        Task<IBaseEntity> GetByIdAsync(Guid id, string key);
        Task<bool> SaveOrUpdateAsync(IBaseEntity entity, string key);
        Task<bool> DeleteAsync(Guid id, string key);

    }
}
