using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Core.DataAccess;
using BlogSite.Entities.ViewModels.CommentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId);

        //Comment GetCommentById(Guid commentId);
        //Task<Comment> GetByIdAsync(Guid id);
        //List<Comment> GetAllComments();
        //Task<List<Comment>> GetAllAsync();
        //List<Comment> GetCommentsByPostId(Guid postId);


        //bool CreateComment(Comment comment);
        //Task<bool> CreateAsync(Comment entity);
        //bool UpdateComment(Comment comment);
        //Task<bool> UpdateAsync(Comment entity);
        //bool DeleteComment(Guid commentId);
        //Task<bool> DeleteAsync(Guid id);

    }
}
