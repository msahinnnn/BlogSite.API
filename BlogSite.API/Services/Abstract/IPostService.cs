using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;

namespace BlogSite.API.Services.Abstract
{
    public interface IPostService
    {
        void CreatePost(CreatePostVM createPostVM, Guid userId);
        List<Post> GetPosts( );

        Task<bool> CreatePostAsync(CreatePostVM createPostVM, Guid userId);
        Task<List<Post>> GetPostsAsync( );
    }
}
