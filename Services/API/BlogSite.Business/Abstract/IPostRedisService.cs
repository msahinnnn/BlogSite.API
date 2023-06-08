using BlogSite.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IPostRedisService
    {
        Task<List<Post>> GetAsync();
        Task<Post> GetByIdAsync(Guid id);
        Task<bool> SaveOrUpdateAsync(Post entity);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Post>> LoadToCacheFromDbAsync();
    }
}
