using BlogSite.API.Models;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Core.DataAccess;
using BlogSite.Core.Entities.Concrete;
using BlogSite.Entities.ViewModels.PostVMs;
using BlogSite.Entities.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.DataAccsess.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> CheckUserEmailExistsAsync(string mail);
        Task<List<OperationClaim>> GetClaims(User user);


    }
}
