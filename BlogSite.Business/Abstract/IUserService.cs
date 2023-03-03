using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        Task<List<User>> GetAllUsersAsync();
        User GetUserById(Guid userId);
        Task<User> GetUserByIdAsync(Guid userId);

        bool CreateUser(CreateUserVM createUserVM);
        Task<bool> CreateUserAsync(CreateUserVM createUserVM);
        bool UpdateUser(UpdateUserVM updateUserVM, Guid userId);
        Task<bool> UpdateUserAsync(UpdateUserVM updateUserVM, Guid userId);
        bool DeleteUser(Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);

    }
}
