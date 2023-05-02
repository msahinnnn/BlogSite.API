using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
using BlogSite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Abstract
{
    public interface ICommentCacheService : IRedisCacheService<Comment>
    {
    }
}
