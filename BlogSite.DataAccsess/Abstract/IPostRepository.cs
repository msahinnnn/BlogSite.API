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

        bool CreatePost(Post post);
        Task<bool> CreatePostAsync(Post post);
        bool UpdatePost(Post post);
        Task<bool> UpdatePostAsync(Post post);
        bool DeletePost(Guid postId);
        Task<bool> DeletePostAsync(Guid postId);


    }
}
