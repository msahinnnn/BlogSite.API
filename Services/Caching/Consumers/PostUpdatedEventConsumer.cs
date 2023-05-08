using Caching.Abstract;
using Caching.Entities;
using MassTransit;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class PostUpdatedEventConsumer : MassTransit.IConsumer<PostUpdatedEvent>
    {
        private IPostCacheService _cacheService;

        public PostUpdatedEventConsumer(IPostCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<PostUpdatedEvent> context)
        {
            Console.WriteLine(context.Message);
            var x = await _cacheService.SaveOrUpdateAsync(new Post()
            {
                Id = context.Message.Id,
                Title = context.Message.Title,
                Content = context.Message.Content,
                CreatedDate = context.Message.CreatedDate,
                UserId = context.Message.UserId,
            });
            Console.WriteLine(nameof(PostUpdatedEventConsumer) + "- worked");

        }
    }
}
