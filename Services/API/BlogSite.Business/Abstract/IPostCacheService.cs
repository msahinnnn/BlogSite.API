﻿using BlogSite.API.Models;
using BlogSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface IPostCacheService 
    {
        Task<List<Post>> GetAsync();
        Task<Post> GetByIdAsync(Guid id);
        Task<bool> SaveOrUpdateAsync(Post entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
