using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Validations;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
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
        private IRedisService _redisService;
        public PostService(IPostRepository postRepository, IMapper mapper, IRedisService redisService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _redisService = redisService;
        }

        public async Task<IDataResult<List<Post>>> GetAllPostsAsync()
        {
            List<Post> posts = await _postRepository.GetAllAsync();
            var res = await _redisService.GetAllCacheAsync<Post>("posts", posts);
            return new DataResult<List<Post>>(res.Data, true, "All Posts...");
        }

        public async Task<IDataResult<Post>> GetPostByIdAsync(Guid postId)
        {
            var res = await _redisService.GetByIdCacheAsync<Post>("posts", postId);
            if (res.Success == true)
            {
                return new DataResult<Post>(res.Data, true, "Post by Id...");
            }
            return new DataResult<Post>(res.Data, false, res.Message);
        }

        public async Task<IDataResult<List<Post>>> GetPostsByUserIdAsync(Guid userId)
        {
            var res = await _postRepository.GetPostsByUserIdAsync(userId);
            return new DataResult<List<Post>>(res, true, "Posts by User Id...");
        }

        public async Task<IResult> CreatePostAsync(CreatePostVM createPostVM)
        {
            ValidationTool.Validate(new PostValidator(), createPostVM);
            Post post = _mapper.Map<Post>(createPostVM);
            post.Id = Guid.NewGuid();
            post.CreatedDate = DateTime.Now;
            post.UserId = createPostVM.UserId;
            var res = await _postRepository.CreateAsync(post);
            if(res == true)
            {
                var cache = await _redisService.CreateCacheAsync<Post>($"posts:{post.Id}", post);
                if (cache.Success == true)
                {
                    return new DataResult<Post>(post, true, "Post successfully created...");
                }
                return new DataResult<Post>(post, true, "Post successfully added to db but couldn' t added to CacheDB...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }

        public async Task<IResult> DeletePostAsync(Guid postId)
        {
            var res = await _postRepository.DeleteAsync(postId);
            if (res == true)
            {
                var cache = await _redisService.DeleteCacheAsync($"posts:{postId}", postId);
                if (cache.Success == true)
                {
                    return new Result(true, "Post successfully deleted...");
                }
                return new Result(true, "Post successfully deleted from DB but couldn't deleted from CacheDB...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }

        public async Task<IResult> UpdatePostAsync(UpdatePostVM updatePostVM, Guid postId)
        {
            Post post = _mapper.Map<Post>(updatePostVM);
            post.Id = postId;
            var res = await _postRepository.UpdateAsync(post);
            if (res == true)
            {
                return new Result(true, "Post successfully updated...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }

        //public List<Post> GetAllPosts()
        //{
        //    var res = _postRepository.GetAllPosts();
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public async Task<List<Post>> GetAllPostsAsync()
        //{
        //    var res = await _postRepository.GetAllPostsAsync();
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public List<Post> GetPostsByUserId(Guid userId)
        //{
        //    var res = _postRepository.GetPostsByUserId(userId);
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public async Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        //{
        //    var res = await _postRepository.GetPostsByUserIdAsync(userId);
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public Post GetPostById(Guid postId)
        //{
        //    var res = _postRepository.GetPostById(postId);
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public async Task<Post> GetPostByIdAsync(Guid postId)
        //{
        //    var res = await _postRepository.GetPostByIdAsync(postId);
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public bool CreatePost(CreatePostVM createPostVM)
        //{
        //    ValidationTool.Validate(new PostValidator(), createPostVM);
        //    Post post = _mapper.Map<Post>(createPostVM);
        //    post.Id = Guid.NewGuid();
        //    post.CreatedDate = DateTime.Now;            
        //    var res = _postRepository.CreatePost(post);
        //    if(res == true)
        //    {
        //        return true;
        //    }
        //    return false;

        //}

        //public async Task<bool> CreatePostAsync(CreatePostVM createPostVM)
        //{
        //    ValidationTool.Validate(new PostValidator(), createPostVM);
        //    Post post = _mapper.Map<Post>(createPostVM);
        //    post.Id = Guid.NewGuid();
        //    post.CreatedDate = DateTime.Now;
        //    var res = await _postRepository.CreatePostAsync(post);
        //    if (res == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool UpdatePost(UpdatePostVM updatePostVM, Guid postId)
        //{
        //    Post post = _mapper.Map<Post>(updatePostVM);
        //    post.Id = postId;
        //    var res = _postRepository.UpdatePost(post);
        //    if (res == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<bool> UpdatePostAsync(UpdatePostVM updatePostVM, Guid postId)
        //{
        //    Post post = _mapper.Map<Post>(updatePostVM);
        //    post.Id = postId;
        //    var res = await _postRepository.UpdatePostAsync(post);
        //    if (res == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool DeletePost(Guid postId)
        //{
        //    var res = _postRepository.DeletePost(postId);
        //    if (res == true)
        //    {
        //        return true;
        //    };
        //    return false;
        //}

        //public async Task<bool> DeletePostAsync(Guid postId)
        //{
        //    var res = await _postRepository.DeletePostAsync(postId);
        //    if (res == true)
        //    {
        //        return true;
        //    };
        //    return false;
        //}
    }
}
