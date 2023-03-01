using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.UserVMs;

namespace BlogSite.API.Services.Abstract
{
    public interface ICommentService
    {
        void CreateComment(CreateCommentVM createCommentVM, Guid postId);
        List<Comment> GetComments();

        Task<bool> CreateCommentAsync(CreateCommentVM createCommentVM, Guid postId);
        Task<List<Comment>> GetCommentsAsync();
    }
}
