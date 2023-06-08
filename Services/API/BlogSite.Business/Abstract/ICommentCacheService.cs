﻿using BlogSite.API.Models;
using BlogSite.Business.Concrete;
using BlogSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Abstract
{
    public interface ICommentCacheService 
    {
        Task<List<Comment>> GetAsync();
        Task<Comment> GetByIdAsync(Guid id);
        Task<bool> SaveOrUpdateAsync(Comment entity);
        Task<bool> DeleteAsync(Guid id);

    }
}
