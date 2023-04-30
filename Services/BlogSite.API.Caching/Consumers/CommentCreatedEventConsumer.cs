using AutoMapper;
using BlogSite.API.Caching.Abstract;
using BlogSite.API.Shared.Messages;
using BlogSite.API.ViewModels.CommentVMs;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class CommentCreatedEventConsumer : MassTransit.IConsumer<CommentCreatedEvent>
    {
        private ICommentCacheService _cacheService;

        public CommentCreatedEventConsumer(ICommentCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<CommentCreatedEvent> context)
        {
            await _cacheService.SaveOrUpdateAsync(context.Message);
        }
    }
}
