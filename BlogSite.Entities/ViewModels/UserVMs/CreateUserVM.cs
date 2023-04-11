using BlogSite.API.Models;
using BlogSite.Core.Entities;
namespace BlogSite.API.ViewModels.UserVMs
{
    public class CreateUserVM : IVM<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
