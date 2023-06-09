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
    public class PostService : IPostService
    {
        private IPostCacheService _postCacheService;

        public PostService(IPostCacheService postCacheService)
        {
            _postCacheService = postCacheService;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _postCacheService.DeleteAsync(PostCacheKeys.PostKey ,id);
        }

        public async Task<List<Post>> GetAsync()
        {
            return await _postCacheService.GetAsync(PostCacheKeys.PostKey);
        }

        public async Task<bool> SaveOrUpdateAsync(Post entity)
        {
            return await _postCacheService.SaveOrUpdateAsync(entity);
        }
    }
}
