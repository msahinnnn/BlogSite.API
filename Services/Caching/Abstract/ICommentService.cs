using Caching.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Abstract
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAsync();
        Task<bool> SaveOrUpdateAsync(Comment entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
