using BlogSite.API.Models;
using BlogSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Entities.ViewModels.UserVMs
{
    public class UpdateUserVM : IVM<User>
    {
        public string Email { get; set; }
    }
}
