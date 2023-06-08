using BlogSite.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface ICommentRedisService
    {
        Task<List<Comment>> GetAsync();
        Task<Comment> GetByIdAsync(Guid id);
        Task<bool> SaveOrUpdateAsync(Comment entity);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Comment>> LoadToCacheFromDbAsync();

    }
}
