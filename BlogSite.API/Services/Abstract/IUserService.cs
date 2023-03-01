using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;

namespace BlogSite.API.Services.Abstract
{
    public interface IUserService
    {
        void CreateUser(CreateUserVM createUserVM);
        List<User> GetUsers();

        Task<bool> CreateUserAsync(CreateUserVM createUserVM);
        Task<List<User>> GetUsersAsync();
    }
}
