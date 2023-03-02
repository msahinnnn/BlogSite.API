using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.CommentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Abstract
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        Task<List<Comment>> GetAllCommentsAsync();
        List<Comment> GetCommentsByPostId(Guid postId);
        Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId);
        Comment GetCommentById(Guid commentId);
        Task<Comment> GetCommentByIdAsync(Guid commentId);

        bool CreateComment(CreateCommentVM createCommentVM);
        Task<bool> CreateCommentAsync(CreateCommentVM createCommentVM);
        bool UpdateComment(UpdateCommentVM updateCommentVM);
        Task<bool> UpdateCommentAsync(UpdateCommentVM updateCommentVM);
        bool DeleteComment(Guid commentId);
        Task<bool> DeleteCommentAsync(Guid commentId);

    }
}
