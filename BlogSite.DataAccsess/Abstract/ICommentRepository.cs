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

        bool CreateComment(Comment comment);
        Task<bool> CreateCommentAsync(Comment comment);
        bool UpdateComment(UpdateCommentVM updateCommentVM, Guid commentId);
        Task<bool> UpdateCommentAsync(UpdateCommentVM updateCommentVM, Guid commentId);
        bool DeleteComment(Guid commentId);
        Task<bool> DeleteCommentAsync(Guid commentId);

    }
}
