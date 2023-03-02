using BlogSite.API.Models;
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
        User GetUserById(Guid userId);
    }
}
