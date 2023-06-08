using BlogSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IRedisService
    {
        Task<List<IBaseEntity>> GetAsync();
        Task<IBaseEntity> GetByIdAsync(Guid id);
        Task<bool> SaveOrUpdateAsync(IBaseEntity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
