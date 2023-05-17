using BlogSite.Core.Entities;
using BlogSite.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Core.Business
{
    public interface IEntityService<T> where T : class, IBaseEntity, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(IVM<T> entityVM);
        Task<bool> UpdateAsync(IVM<T> entityVM, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
