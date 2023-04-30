using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
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
using MassTransit;
using Microsoft.AspNetCore.Http;
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
        private IAuthService _authService;
        private readonly IPublishEndpoint _publishEndpoint;


        public PostService(IPostRepository postRepository, IMapper mapper, IAuthService authService, IPublishEndpoint publishEndpoint)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _authService = authService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IDataResult<List<Post>>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return new SuccessDataResult<List<Post>>(posts, PostMessages.PostsListed);
        }

        public async Task<IDataResult<Post>> GetByIdAsync(Guid id)
        {
            var res = await _postRepository.GetByIdAsync(id);
            return new SuccessDataResult<Post>(res, PostMessages.PostsListed);
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
            post.UserId = Guid.Parse(_authService.GetCurrentUserId());
            post.CreatedDate = DateTime.Now;
            var res = await _postRepository.CreateAsync(post);
            if (res is not null)
            {
                await _publishEndpoint.Publish<PostCreatedEvent>(new PostCreatedEvent()
                {
                    Id = post.Id,
                    CreatedDate = post.CreatedDate,
                    Title = post.Title,
                    Content = post.Content  ,
                    UserId = post.UserId
                });
                return new SuccessDataResult<Post>(post, PostMessages.PostAdded);             
            }
            return new ErrorDataResult<Post>(res, PostMessages.PostAddedError);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var check = await _postRepository.GetByIdAsync(id);
            var userAuth =  Guid.Parse(_authService.GetCurrentUserId());
            if(check.UserId == userAuth)
            {
                var res = await _postRepository.DeleteAsync(id);
                if (res == true)
                {
                    await _publishEndpoint.Publish<PostDeletedEvent>(new PostDeletedEvent()
                    {
                        Id = check.Id,
                    });
                    return new SuccessResult(PostMessages.PostRemoved);
                }
                return new ErrorResult(PostMessages.PostRemovedError);
            }
            return new ErrorResult(AuthMessages.UnAuthorizationMessage);
        }

        public async Task<IResult> UpdateAsync(IVM<Post> entityVM, Guid id)
        {
            Post post = _mapper.Map<Post>(entityVM);
            var check = await _postRepository.GetByIdAsync(id);
            var userAuth = Guid.Parse(_authService.GetCurrentUserId());
            post.Id = id;
            post.UserId = userAuth;
            if (check.UserId == userAuth)
            {
                var res = await _postRepository.UpdateAsync(post);
                if (res == true)
                {
                    await _publishEndpoint.Publish<PostUpdatedEvent>(new PostUpdatedEvent()
                    {
                        Id = post.Id,
                        CreatedDate = post.CreatedDate,
                        Title = post.Title,
                        Content = post.Content,
                        UserId = post.UserId
                    });
                    return new SuccessResult(PostMessages.PostUpdated);
                }
                return new ErrorResult(PostMessages.PostUpdatedError);
            }
            return new ErrorResult(AuthMessages.UnAuthorizationMessage);
        }

    }
}
