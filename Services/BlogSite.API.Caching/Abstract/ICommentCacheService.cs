using BlogSite.API.Models;
using BlogSite.Core.Services;
using BlogSite.DataAccsess.Concrete.AdoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Abstract
{
    public interface ICommentCacheService : Core.Services.IRedisCacheService<Comment, CommentRepository>
    {
    }
}
