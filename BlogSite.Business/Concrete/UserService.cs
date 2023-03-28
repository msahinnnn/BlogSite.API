using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Validations;
using BlogSite.Core.Entities;
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
            return new DataResult<List<User>>(res, true, "All Users...");
        }

        public async Task<IDataResult<User>> GetByIdAsync(Guid id)
        {
            var res = await _userRepository.GetByIdAsync(id);
            return new DataResult<User>(res, true, "User by Id...");
        }
        public async Task<IDataResult<User>> CreateAsync(IVM<User> entityVM)
        {
            ValidationTool.Validate(new UserValidator(), entityVM);
            User user = _mapper.Map<User>(entityVM);
            user.Id = Guid.NewGuid();
            var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
            if (!check)
            {
                var res = await _userRepository.CreateAsync(user);
                if (res is not null)
                {
                    return new DataResult<User>(user, true, "User successfully created...");
                }
                return new DataResult<User>(user, false, "Something went wrong! Please try again.");
            }
            return new DataResult<User>(user, false, "User already exists!");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var res = await _userRepository.DeleteAsync(id);
            if (res == true)
            {
                return new Result(true, "User successfully deleted...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }

        public async Task<IResult> UpdateAsync(IVM<User> entityVM, Guid id)
        {
            User user = _mapper.Map<User>(entityVM);
            user.Id = id;
            var res = await _userRepository.UpdateAsync(user);
            if (res == true)
            {
                return new Result(true, "User successfully updated...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }



        //public async Task<IDataResult<List<User>>> GetAllUsersAsync()
        //{
        //    var res = await _userRepository.GetAllAsync();
        //    return new DataResult<List<User>>(res, true, "All Users...");
        //}

        //public async Task<IDataResult<User>> GetUserByIdAsync(Guid userId)
        //{
        //    var res = await _userRepository.GetByIdAsync(userId);
        //    return new DataResult<User>(res, true, "User by Id...");
        //}

        //public async Task<IResult> CreateUserAsync(CreateUserVM createUserVM)
        //{
        //    ValidationTool.Validate(new UserValidator(), createUserVM);
        //    User user = _mapper.Map<User>(createUserVM);
        //    user.Id = Guid.NewGuid();
        //    var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
        //    if (!check)
        //    {
        //        var res = await _userRepository.CreateAsync(user);
        //        if (res == true)
        //        {
        //            return new Result(true, "User successfully created...");
        //        }
        //        return new Result(false, "Something went wrong! Please try again.");
        //    }
        //    return new Result(false, "User already exists!");
        //}

        //public async Task<IResult> DeleteUserAsync(Guid userId)
        //{
        //    var res = await _userRepository.DeleteAsync(userId);
        //    if (res == true)
        //    {
        //        return new Result(true, "User successfully deleted...");
        //    }
        //    return new Result(false, "Something went wrong! Please try again.");
        //}

        //public async Task<IResult> UpdateUserAsync(UpdateUserVM updateUserVM, Guid userId)
        //{
        //    User user = _mapper.Map<User>(updateUserVM);
        //    user.Id = userId;
        //    var res = await _userRepository.UpdateAsync(user);
        //    if (res == true)
        //    {
        //        return new Result(true, "User successfully updated...");
        //    }
        //    return new Result(false, "Something went wrong! Please try again.");
        //}



    }
}
