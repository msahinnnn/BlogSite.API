using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Core.Business;
using BlogSite.Core.Utilities.Results;
using BlogSite.Entities.ViewModels.CommentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface ICommentService : IEntityService<Comment>
    {
        //Task<IDataResult<List<Comment>>> GetAllCommentsAsync();
        Task<IDataResult<List<Comment>>> GetCommentsByPostIdAsync(Guid postId);
        //Task<IDataResult<Comment>> GetCommentByIdAsync(Guid commentId);

        //Task<IDataResult<Comment>> CreateCommentAsync(CreateCommentVM createCommentVM);
        //Task<IResult> UpdateCommentAsync(UpdateCommentVM updateCommentVM, Guid commentId);
        //Task<IResult> DeleteCommentAsync(Guid commentId);
    }
}
