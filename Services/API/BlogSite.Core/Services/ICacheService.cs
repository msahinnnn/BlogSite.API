using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Core.Services
{
    public interface ICacheService<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> SaveOrUpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
