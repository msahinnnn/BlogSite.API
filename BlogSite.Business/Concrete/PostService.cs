using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.Core.Entities;
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

        public async Task<IDataResult<List<Post>>> GetAllAsync()
        {
            List<Post> posts = await _postRepository.GetAllAsync();
            var res = await _redisService.GetAllCacheAsync<Post>(PostCacheKeys.PostCacheKey, posts);
            return new SuccessDataResult<List<Post>>(res.Data, PostMessages.PostsListed);
        }

        public async Task<IDataResult<Post>> GetByIdAsync(Guid id)
        {
            var res = await _redisService.GetByIdCacheAsync<Post>(PostCacheKeys.PostCacheKey, id);
            if (res.Success == true)
            {
                return new SuccessDataResult<Post>(res.Data, PostMessages.PostsListed);
            }
            return new ErrorDataResult<Post>(res.Data, PostMessages.PostsListedError);
        }

        public async Task<IDataResult<List<Post>>> GetPostsByUserIdAsync(Guid userId)
        {
            var res = await _postRepository.GetPostsByUserIdAsync(userId);
            return new SuccessDataResult<List<Post>>(res, PostMessages.PostsListed);
        }

        public async Task<IDataResult<Post>> CreateAsync(IVM<Post> entityVM)
        {
            ValidationTool.Validate(new PostValidator(), entityVM);
            Post post = _mapper.Map<Post>(entityVM);
            post.Id = Guid.NewGuid();
            post.CreatedDate = DateTime.Now;
            var res = await _postRepository.CreateAsync(post);
            if (res is not null)
            {
                var cache = await _redisService.CreateCacheAsync<Post>($"post-{post.Id}", post);
                if (cache.Success == true)
                {
                    return new SuccessDataResult<Post>(post, PostMessages.PostAdded);
                }
                return new ErrorDataResult<Post>(post, PostMessages.PostAddedCacheError);
            }
            return new ErrorDataResult<Post>(res, PostMessages.PostAddedError);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var res = await _postRepository.DeleteAsync(id);
            if (res == true)
            {
                var cache = await _redisService.DeleteCacheAsync($"post-{id}", id);
                if (cache.Success == true)
                {
                    return new SuccessResult(PostMessages.PostRemoved);
                }
                return new ErrorResult(PostMessages.PostRemovedCacheError);
            }
            return new ErrorResult(PostMessages.PostRemovedError);
        }

        public async Task<IResult> UpdateAsync(IVM<Post> entityVM, Guid id)
        {
            Post post = _mapper.Map<Post>(entityVM);
            post.Id = id;
            var res = await _postRepository.UpdateAsync(post);
            if (res == true)
            {
                return new SuccessResult(PostMessages.PostUpdated);
            }
            return new ErrorResult(PostMessages.PostUpdatedError);
        }

    }
}
