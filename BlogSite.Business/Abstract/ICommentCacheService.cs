using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface ICommentCacheService
    {
        Task<IDataResult<Comment>> GetByIdAsync(Guid id);

        Task<IDataResult<Comment>> CreateAsync(CreateCommentVM createCommentVM);

        Task<IResult> RemoveAsync(Guid id);
    }
}
