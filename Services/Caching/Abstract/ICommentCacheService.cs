using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
using BlogSite.Core.Services;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Concrete.AdoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Abstract
{
    public interface ICommentCacheService : IRedisCacheService<Comment>
    {

    }
}
