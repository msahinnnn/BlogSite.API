using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface ICommentCacheService
    {
        Task<List<Comment>> GetAsync();

        Task<Comment> GetByIdAsync(Guid id);

        Task<bool> CreateAsync(CreateCommentVM createCommentVM);
    }
}
