using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Concrete.AdoNet
{
    public class PostRepository : IPostRepository
    {
        public bool CheckPostTitleExists(string title)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPostTitleExistsAsync(string title)
        {
            throw new NotImplementedException();
        }

        public bool CreatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPostsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(UpdatePostVM updatePostVM, Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(UpdatePostVM updatePostVM, Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
