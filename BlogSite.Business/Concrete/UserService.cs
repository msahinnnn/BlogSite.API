using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Validations;
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

        public async Task<IDataResult<List<User>>> GetAllUsersAsync()
        {
            var res = await _userRepository.GetAllAsync();
            return new DataResult<List<User>>(res, true, "All Users...");
        }

        public async Task<IDataResult<User>> GetUserByIdAsync(Guid userId)
        {
            var res = await _userRepository.GetByIdAsync(userId);
            return new DataResult<User>(res, true, "User by Id...");
        }

        public async Task<IResult> CreateUserAsync(CreateUserVM createUserVM)
        {
            ValidationTool.Validate(new UserValidator(), createUserVM);
            User user = _mapper.Map<User>(createUserVM);
            user.Id = Guid.NewGuid();
            var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
            if (!check)
            {
                var res = await _userRepository.CreateAsync(user);
                if (res == true)
                {
                    return new Result(true, "User successfully created...");
                }
                return new Result(false, "Something went wrong! Please try again.");
            }
            return new Result(false, "User already exists!");
        }

        public async Task<IResult> DeleteUserAsync(Guid userId)
        {
            var res = await _userRepository.DeleteAsync(userId);
            if (res == true)
            {
                return new Result(true, "User successfully deleted...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }

        public async Task<IResult> UpdateUserAsync(UpdateUserVM updateUserVM, Guid userId)
        {
            User user = _mapper.Map<User>(updateUserVM);
            user.Id = userId;
            var res = await _userRepository.UpdateAsync(user);
            if (res == true)
            {
                return new Result(true, "User successfully updated...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }


        //public List<User> GetAllUsers()
        //{
        //    var res = _userRepository.GetAllUsers();
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public async Task<List<User>> GetAllUsersAsync()
        //{
        //    var res = await _userRepository.GetAllUsersAsync();
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public User GetUserById(Guid userId)
        //{
        //    var res = _userRepository.GetUserById(userId);
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public async Task<User> GetUserByIdAsync(Guid userId)
        //{
        //    var res = await _userRepository.GetUserByIdAsync(userId);
        //    if (res is not null)
        //    {
        //        return res;
        //    }
        //    return null;
        //}

        //public bool CreateUser(CreateUserVM createUserVM)
        //{
        //    ValidationTool.Validate(new UserValidator(), createUserVM);
        //    User user = _mapper.Map<User>(createUserVM);
        //    user.Id = Guid.NewGuid();
        //    var check = _userRepository.CheckUserEmailExists(user.Email);
        //    if (check)
        //    {
        //        return false;
        //    }
        //    var res = _userRepository.CreateUser(user);
        //    if (res == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<bool> CreateUserAsync(CreateUserVM createUserVM)
        //{
        //    ValidationTool.Validate(new UserValidator(), createUserVM);
        //    User user = _mapper.Map<User>(createUserVM);
        //    user.Id = Guid.NewGuid();
        //    var check = await _userRepository.CheckUserEmailExistsAsync(user.Email);
        //    if (check)
        //    {
        //        return false;
        //    }
        //    var res = await _userRepository.CreateUserAsync(user);
        //    if (res == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool UpdateUser(UpdateUserVM updateUserVM, Guid userId)
        //{
        //    User user = _mapper.Map<User>(updateUserVM);
        //    user.Id = userId;
        //    var res = _userRepository.UpdateUser(user);
        //    if (res == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<bool> UpdateUserAsync(UpdateUserVM updateUserVM, Guid userId)
        //{
        //    User user = _mapper.Map<User>(updateUserVM);
        //    user.Id = userId;
        //    var res = await _userRepository.UpdateUserAsync(user);
        //    if (res == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool DeleteUser(Guid userId)
        //{
        //    var res = _userRepository.DeleteUser(userId);
        //    if (res == true)
        //    {
        //        return true;
        //    };
        //    return false;
        //}

        //public async Task<bool> DeleteUserAsync(Guid userId)
        //{
        //    var res = await _userRepository.DeleteUserAsync(userId);
        //    if (res == true)
        //    {
        //        return true;
        //    };
        //    return false;
        //}
    }
}
