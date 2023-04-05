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


    }
}
