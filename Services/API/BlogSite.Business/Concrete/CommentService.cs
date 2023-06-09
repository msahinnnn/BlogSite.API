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
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IMapper _mapper;
        private IAuthService _authService;


        public CommentService(ICommentRepository commentRepository, IMapper mapper, IAuthService authService )
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _authService = authService;
        }


        public async Task<List<Comment>> GetAllAsync()
        {

            var res = await _commentRepository.GetAllAsync();
            return res;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var res = await _commentRepository.GetByIdAsync(id);
            return res;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            var res = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return res;
        }

        public async Task<Comment> CreateAsync(IVM<Comment> entityVM)
        {
            ValidationTool.Validate(new CommentValidator(), entityVM);
            Comment comment = _mapper.Map<Comment>(entityVM);
            comment.Id = Guid.NewGuid();
            comment.CreateTime = DateTime.Now;
            var res = await _commentRepository.CreateAsync(comment);
            return res;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var check = await _commentRepository.GetByIdAsync(id);
            var res = await _commentRepository.DeleteAsync(id);
            if (res == true)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(IVM<Comment> entityVM, Guid id)
        {
            Comment comment = _mapper.Map<Comment>(entityVM);
            var check = await _commentRepository.GetByIdAsync(id);
            comment.Id = id;
            var res = await _commentRepository.UpdateAsync(comment);
            if(res == true)
            {
                return true;
            }
            return false;
        }

    }
}
