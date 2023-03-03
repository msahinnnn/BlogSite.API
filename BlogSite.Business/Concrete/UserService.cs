using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Validations;
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

        public List<User> GetAllUsers()
        {
            var res = _userRepository.GetAllUsers();
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid userId)
        {
            var res = _userRepository.GetUserById(userId);
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public Task<User> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(CreateUserVM createUserVM)
        {
            ValidationTool.Validate(new UserValidator(), createUserVM);
            User user = _mapper.Map<User>(createUserVM);
            user.Id = Guid.NewGuid();
            var check = _userRepository.CheckUserEmailExists(user.Email);
            if (check)
            {
                return false;
            }
            var res = _userRepository.CreateUser(user);
            if (res == true)
            {
                return true;
            }
            return false;
        }

        public Task<bool> CreateUserAsync(CreateUserVM createUserVM)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UpdateUserVM updateUserVM, Guid userId)
        {
            User user = _mapper.Map<User>(updateUserVM);
            user.Id = userId;
            var res = _userRepository.UpdateUser(user);
            if (res == true)
            {
                return true;
            }
            return false;
        }

        public Task<bool> UpdateUserAsync(UpdateUserVM updateUserVM, Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(Guid userId)
        {
            var res = _userRepository.DeleteUser(userId);
            if (res == true)
            {
                return true;
            };
            return false;
        }

        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
