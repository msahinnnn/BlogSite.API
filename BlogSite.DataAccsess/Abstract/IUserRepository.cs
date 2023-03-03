using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Abstract
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        Task<List<User>> GetAllUsersAsync();
        User GetUserById(Guid userId);
        Task<User> GetUserByIdAsync(Guid userId);

        bool CreateUser(User user);
        Task<bool> CreateUserAsync(User user);
        bool UpdateUser(User user);
        Task<bool> UpdateUserAsync(User user);
        bool DeleteUser(Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);

        bool CheckUserEmailExists(string mail);
        Task<bool> CheckUserEmailExistsAsync(string mail);
    }
}
