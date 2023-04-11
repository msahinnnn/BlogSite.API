using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Core.Entities.Concrete;
using BlogSite.Core.Security.JWT;
using BlogSite.Core.Utilities.Results;
using BlogSite.Entities.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> RegisterAsync(CreateUserVM createUserVM, string password);
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);
        Task<IDataResult<User>> LoginAsync(LoginUserVM loginUserVM);
        Task<List<OperationClaim>> GetClaims(User user);
    }
}
