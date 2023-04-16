using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Authentication;
using BlogSite.Core.Business;
using BlogSite.Core.Entities;
using BlogSite.Core.Utilities.Results;
using BlogSite.Entities.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IAuthService 
    {
        Task<IDataResult<User>> RegisterAsync(CreateUserVM createUserVM);
        Task<IDataResult<TokenDto>> LoginAsync(LoginUserVM loginUserVM);
        Task<IDataResult<TokenDto>> RefreshAsync(TokenDto tokenDto);
        string GetCurrentUserId();
        string GetCurrentUserMail();
        string GetCurrentUserRole();
    }
}
