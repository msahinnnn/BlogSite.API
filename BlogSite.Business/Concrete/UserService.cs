using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.Core.Entities;
using BlogSite.Core.Entities.Concrete;
using BlogSite.Core.Security.Hashing;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using FluentValidation;
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

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
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
            //ValidationTool.Validate(new UserValidator(), entityVM);
            User user = _mapper.Map<User>(entityVM);
            user.Id = Guid.NewGuid();
            var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
            if (check is null)
            {
                var res = await _userRepository.CreateAsync(user);
                if (res is not null)
                {
                    return new SuccessDataResult<User>(user, UserMessages.UserAdded);
                }
                return new ErrorDataResult<User>(user, UserMessages.UserAddedError);
            }
            return new ErrorDataResult<User>(user, UserMessages.UserAldreadyExistsError);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var res = await _userRepository.DeleteAsync(id);
            if (res == true)
            {
                return new SuccessResult(RedisMessages.ItemDeleted);
            }
            return new ErrorResult(RedisMessages.ItemDeletedError);
        }

        public async Task<IResult> UpdateAsync(IVM<User> entityVM, Guid id)
        {
            User user = _mapper.Map<User>(entityVM);
            user.Id = id;
            var res = await _userRepository.UpdateAsync(user);
            if (res == true)
            {
                return new SuccessResult(UserMessages.UserUpdated);
            }
            return new ErrorResult(UserMessages.UserUpdatedError);
        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            return await _userRepository.GetClaims(user);
        }

        public async Task<User> EmailExists(string email)
        {
            return await _userRepository.CheckUserEmailExistsAsync(email);
        }
    }
}
