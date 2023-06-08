using BlogSite.API.Models;
using BlogSite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface ICommentCacheService : IRedisService
    {
    }
}
