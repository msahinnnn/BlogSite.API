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
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IMapper _mapper;
        private IAuthService _authService;


        public PostService(IPostRepository postRepository, IMapper mapper, IAuthService authService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _authService = authService;

        }

        public async Task<List<Post>> GetAllAsync()
        {

            var posts = await _postRepository.GetAllAsync();
            return posts;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var res = await _postRepository.GetByIdAsync(id);
            return res;
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            var res = await _postRepository.GetPostsByUserIdAsync(userId);
            return res;
        }

        public async Task<Post> CreateAsync(IVM<Post> entityVM)
        {
            ValidationTool.Validate(new PostValidator(), entityVM);
            Post post = _mapper.Map<Post>(entityVM);
            post.Id = Guid.NewGuid();
            post.UserId = Guid.Parse(_authService.GetCurrentUserId());
            post.CreatedDate = DateTime.Now;
            var res = await _postRepository.CreateAsync(post);
            return res;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            //var check = await _postRepository.GetByIdAsync(id);
            //var userAuth =  Guid.Parse(_authService.GetCurrentUserId());
            //if(check.UserId == userAuth)
            //{
                var res = await _postRepository.DeleteAsync(id);
                return true;
            //}
            //return false;
        }

        public async Task<bool> UpdateAsync(IVM<Post> entityVM, Guid id)
        {
            Post post = _mapper.Map<Post>(entityVM);
            var check = await _postRepository.GetByIdAsync(id);
            var userAuth = Guid.Parse(_authService.GetCurrentUserId());
            post.Id = id;
            post.UserId = userAuth;
                //if (check.UserId == userAuth)
                //{
                    var res = await _postRepository.UpdateAsync(post);
                if (res == true)
                {
                    return true;
                }
                return false;
            //}
            //return false;
        }

    }
}
