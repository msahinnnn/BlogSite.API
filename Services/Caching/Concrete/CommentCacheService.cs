using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
using Caching.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Concrete
{
    public class CommentCacheService : ICommentCacheService
    {
        public Task<bool> DeleteAsync(string key, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAsync(string key)
        {
            throw new NotImplementedException();
        }


        public Task<bool> SaveOrUpdateAsync(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
