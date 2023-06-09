using Caching.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Abstract
{
    public interface IPostService
    {
        Task<List<Post>> GetAsync();
        Task<bool> SaveOrUpdateAsync(Post entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
