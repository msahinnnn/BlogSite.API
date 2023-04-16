using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.Core.Entities;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ILogger<UserService> _logger;
        private ITokenHandler _tokenHandler;
        private IAuthService _authService;

        public UserService(IMapper mapper, IUserRepository userRepository, ILogger<UserService> logger, ITokenHandler tokenHandler, IAuthService authService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
            _tokenHandler = tokenHandler;
            _authService = authService;
        }

        public async Task<IDataResult<List<User>>> GetAllAsync()
        {
            var res = await _userRepository.GetAllAsync();
            return new SuccessDataResult<List<User>>(res, UserMessages.UsersListed);
        }

        public async Task<IDataResult<User>> GetByIdAsync(Guid id)
        {
            var res = await _userRepository.GetByIdAsync(id);
            return new SuccessDataResult<User>(res, UserMessages.UsersListed);
        }
        public async Task<IDataResult<User>> CreateAsync(IVM<User> entityVM)
        {
            ValidationTool.Validate(new UserValidator(), entityVM);
            User user = _mapper.Map<User>(entityVM);
            user.Id = Guid.NewGuid();
            user.Token = _tokenHandler.CreateToken(user, UserRoles.User);
            user.RefreshToken = _tokenHandler.CreateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);
            var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
            if (check is null)
            {
       
                var res = await _userRepository.CreateAsync(user);
                if (res is not null)
                {
                    return new SuccessDataResult<User>(user, UserMessages.UserAdded);
                }
                _logger.LogError(UserMessages.UserAddedError);
                return new ErrorDataResult<User>(user, UserMessages.UserAddedError);
            }
            _logger.LogError(UserMessages.UserAldreadyExistsError);
            return new ErrorDataResult<User>(user, UserMessages.UserAldreadyExistsError);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var check = await _userRepository.GetByIdAsync(id);
            var res = await _userRepository.DeleteAsync(id);
            var userAuth = Guid.Parse(_authService.GetCurrentUserId());
            if(check.Id  == userAuth)
            {
                if (res == true)
                {
                    return new SuccessResult(RedisMessages.ItemDeleted);
                }
                _logger.LogError(RedisMessages.ItemDeletedError);
                return new ErrorResult(RedisMessages.ItemDeletedError);
            }
            return new ErrorResult(AuthMessages.UnAuthorizationMessage);
        }

        public async Task<IResult> UpdateAsync(IVM<User> entityVM, Guid id)
        {
            User user = _mapper.Map<User>(entityVM);
            user.Id = id;
            var check = await _userRepository.GetByIdAsync(id);
            var userAuth = Guid.Parse(_authService.GetCurrentUserId());
            if(check.Id == userAuth)
            {
                var res = await _userRepository.UpdateAsync(user);
                if (res == true)
                {
                    return new SuccessResult(UserMessages.UserUpdated);
                }
                _logger.LogError(UserMessages.UserUpdatedError);
                return new ErrorResult(UserMessages.UserUpdatedError);
            }
            return new ErrorResult(AuthMessages.UnAuthorizationMessage);
        }

        public async Task<User> EmailExists(string email)
        {
            return await _userRepository.CheckUserEmailExistsAsync(email);
        }
    }
}
