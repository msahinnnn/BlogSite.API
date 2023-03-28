using BlogSite.API.Models;
using BlogSite.Core.Entities;
namespace BlogSite.API.ViewModels.UserVMs
{
    public class CreateUserVM : IVM<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
