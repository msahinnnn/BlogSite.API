using BlogSite.API.Models;
using BlogSite.Core.Entities;
using BlogSite.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Entities.ViewModels.UserVMs
{
    public class LoginUserVM : IVM<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
