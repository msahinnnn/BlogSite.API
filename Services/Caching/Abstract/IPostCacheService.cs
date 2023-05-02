using BlogSite.API.Shared.Messages;
using BlogSite.Core.Services;
using Caching.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Abstract
{
    public interface IPostCacheService : IRedisCacheService<Post>
    {
    }
}
