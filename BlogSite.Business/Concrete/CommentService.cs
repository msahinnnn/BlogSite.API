using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.Core.Entities;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IMapper _mapper;
        private IRedisService _redisService;
        private IAuthService _authService;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IRedisService redisService, IAuthService authService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _redisService = redisService;
            _authService = authService;
        }


        public async Task<IDataResult<List<Comment>>> GetAllAsync()
        {
            List<Comment> comments = await _commentRepository.GetAllAsync();
            var res = await _redisService.GetAllCacheAsync<Comment>(CommentCacheKeys.CommentCacheKey, comments);
            if (res.Success == true)
            {
                return new SuccessDataResult<List<Comment>>(res.Data, CommentMessages.CommentsListed);
            }
            return new ErrorDataResult<List<Comment>>(res.Data, CommentMessages.CommentsListedError);
        }

        public async Task<IDataResult<Comment>> GetByIdAsync(Guid id)
        {
            var res = await _redisService.GetByIdCacheAsync<Comment>(CommentCacheKeys.CommentCacheKey, id);
            if (res.Success == true)
            {
                return new SuccessDataResult<Comment>(res.Data, CommentMessages.CommentsListed);
            }
            return new ErrorDataResult<Comment>(res.Data, CommentMessages.CommentsListedError);
        }

        public async Task<IDataResult<List<Comment>>> GetCommentsByPostIdAsync(Guid postId)
        {
            var res = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return new SuccessDataResult<List<Comment>>(res, CommentMessages.CommentsListedError);
        }

        public async Task<IDataResult<Comment>> CreateAsync(IVM<Comment> entityVM)
        {
            ValidationTool.Validate(new CommentValidator(), entityVM);
            Comment comment = _mapper.Map<Comment>(entityVM);
            comment.Id = Guid.NewGuid();
            comment.CreateTime = DateTime.Now;
            comment.UserId = Guid.Parse(_authService.GetCurrentUserId());
            var res = await _commentRepository.CreateAsync(comment);
            if (res is not null)
            {
                var cache = await _redisService.CreateCacheAsync<Comment>($"comment-{comment.Id}", comment);
                if (cache.Success == true)
                {
                    return new SuccessDataResult<Comment>(comment, CommentMessages.CommentAdded);
                }
                return new ErrorDataResult<Comment>(comment, CommentMessages.CommentAddedCacheError);
            }
            return new ErrorDataResult<Comment>(null, CommentMessages.CommentAddedError);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var check = await _commentRepository.GetByIdAsync(id);
            var userAuth = Guid.Parse(_authService.GetCurrentUserId());
            if(check.UserId == userAuth)
            {
                var res = await _commentRepository.DeleteAsync(id);
                if (res == true)
                {
                    var cache = await _redisService.DeleteCacheAsync(CommentCacheKeys.CommentCacheKey, id);
                    if (cache.Success == true)
                    {
                        return new SuccessResult(CommentMessages.CommentRemoved);
                    }
                    return new ErrorResult(CommentMessages.CommentRemovedCacheError);
                }
                return new ErrorResult(CommentMessages.CommentRemovedError);
            }
            return new ErrorResult(AuthMessages.UnAuthorizationMessage);
        }
        public async Task<IResult> UpdateAsync(IVM<Comment> entityVM, Guid id)
        {
            Comment comment = _mapper.Map<Comment>(entityVM);
            var check = await _commentRepository.GetByIdAsync(id);
            var userAuth = Guid.Parse(_authService.GetCurrentUserId());
            comment.Id = id;
            comment.UserId = userAuth;
            if(check.UserId == userAuth)
            {
                var res = await _commentRepository.UpdateAsync(comment);
                if (res == true)
                {
                    return new SuccessResult(CommentMessages.CommentUpdated);
                }
                return new ErrorResult(CommentMessages.CommentUpdatedError);
            }
            return new ErrorResult(AuthMessages.UnAuthorizationMessage);

        }

    }
}
