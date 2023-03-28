using BlogSite.API.Models;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Core.Business;
using BlogSite.Core.Utilities.Results;
using BlogSite.Entities.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IUserService : IEntityService<User>
    {
        //Task<IDataResult<List<User>>> GetAllUsersAsync();
        //Task<IDataResult<User>> GetUserByIdAsync(Guid userId);

        //Task<IResult> CreateUserAsync(CreateUserVM createUserVM);
        //Task<IResult> UpdateUserAsync(UpdateUserVM updateUserVM, Guid userId);
        //Task<IResult> DeleteUserAsync(Guid userId);

    }
}
