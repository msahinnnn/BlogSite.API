using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Abstract
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        Task<List<Post>> GetAllPostsAsync();
        List<Post> GetPostsByUserId(Guid userId);
        Task<List<Post>> GetPostsByUserIdAsync(Guid userId);
        Post GetPostById(Guid postId);
        Task<Post> GetPostByIdAsync(Guid postId);

        bool CreatePost(CreatePostVM createPostVM);
        Task<bool> CreatePostAsync(CreatePostVM createPostVM);
        bool UpdatePost(UpdatePostVM updatePostVM);
        Task<bool> UpdatePostAsync(UpdatePostVM updatePostVM);
        bool DeletePost(Guid postId);
        Task<bool> DeletePostAsync(Guid postId);

        bool CheckPostTitleExists(string title);
        Task<bool> CheckPostTitleExistsAsync(string title);

    }
}
