using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Validations;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IMapper _mapper;
        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public List<Post> GetAllPosts()
        {
            var res = _postRepository.GetAllPosts();
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public Task<List<Post>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPostsByUserId(Guid userId)
        {
            var res = _postRepository.GetPostsByUserId(userId);
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(Guid postId)
        {
            var res = _postRepository.GetPostById(postId);
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public Task<Post> GetPostByIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public bool CreatePost(CreatePostVM createPostVM)
        {
            ValidationTool.Validate(new PostValidator(), createPostVM);
            Post post = _mapper.Map<Post>(createPostVM);
            post.Id = Guid.NewGuid();
            post.CreatedDate = DateTime.Now;            
            var res = _postRepository.CreatePost(post);
            if(res == true)
            {
                return true;
            }
            return false;

        }

        public Task<bool> CreatePostAsync(CreatePostVM createPostVM)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(UpdatePostVM updatePostVM, Guid postId)
        {
            Post post = _mapper.Map<Post>(updatePostVM);
            post.Id = postId;
            var res = _postRepository.UpdatePost(post);
            if (res == true)
            {
                return true;
            }
            return false;
        }

        public Task<bool> UpdatePostAsync(UpdatePostVM updatePostVM, Guid postId)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(Guid postId)
        {
            var res = _postRepository.DeletePost(postId);
            if (res == true)
            {
                return true;
            };
            return false;
        }

        public Task<bool> DeletePostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
