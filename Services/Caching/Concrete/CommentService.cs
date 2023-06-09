using BlogSite.Business.Constants;
using Caching.Abstract;
using Caching.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Concrete
{
    public class CommentService : ICommentService
    {
        private ICommentCacheService _commentCacheService;

        public CommentService(ICommentCacheService commentCacheService)
        {
            _commentCacheService = commentCacheService;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _commentCacheService.DeleteAsync(CommentCacheKeys.CommentKey, id);
        }

        public async Task<List<Comment>> GetAsync()
        {
            return await _commentCacheService.GetAsync(CommentCacheKeys.CommentKey);
        }

        public async Task<bool> SaveOrUpdateAsync(Comment entity)
        {
            return await _commentCacheService.SaveOrUpdateAsync(entity);
        }
    }
}
