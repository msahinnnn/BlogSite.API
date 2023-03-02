using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Concrete.AdoNet
{
    public class UserRepository : IUserRepository
    {
        public bool CheckUserEmailExists(string mail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUserEmailExistsAsync(string mail)
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UpdateUserVM updateUserVM, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UpdateUserVM updateUserVM, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
