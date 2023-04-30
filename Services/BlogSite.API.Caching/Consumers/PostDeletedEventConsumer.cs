﻿using BlogSite.API.Caching.Abstract;
using BlogSite.API.Shared.Messages;
using BlogSite.Business.Constants;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class PostDeletedEventConsumer : MassTransit.IConsumer<PostDeletedEvent>
    {
        private IPostCacheService _cacheService;

        public PostDeletedEventConsumer(IPostCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<PostDeletedEvent> context)
        {
            await _cacheService.DeleteAsync(PostCacheKeys.PostKey, context.Message.Id);
        }
    }
}
