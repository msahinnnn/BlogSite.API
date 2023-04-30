using BlogSite.API.Caching.Abstract;
using BlogSite.API.Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class PostCreatedEventConsumer : MassTransit.IConsumer<PostCreatedEvent>
    {
        private IPostCacheService _cacheService;

        public PostCreatedEventConsumer(IPostCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            await _cacheService.SaveOrUpdateAsync(context.Message);
        }
    }
}
