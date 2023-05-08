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
        Task<IDataResult<List<T>>> GetAllAsync();
        Task<IDataResult<T>> GetByIdAsync(Guid id);
        Task<IDataResult<T>> CreateAsync(IVM<T> entityVM);
        Task<IResult> UpdateAsync(IVM<T> entityVM, Guid id);
        Task<IResult> DeleteAsync(Guid id);
    }
}
