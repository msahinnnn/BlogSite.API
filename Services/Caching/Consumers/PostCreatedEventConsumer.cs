using BlogSite.API.Shared.Messages;
using BlogSite.Business.Constants;
using BlogSite.Core.Services;
using Caching.Abstract;
using Caching.Concrete;
using Caching.Entities;
using MassTransit;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Caching.Consumers
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
            Console.WriteLine(context.Message);
            var x = await _cacheService.SaveOrUpdateAsync(new Post()
            {
                Id = context.Message.Id,
                Title = context.Message.Title,
                Content = context.Message.Content,
                CreatedDate = context.Message.CreatedDate,
                UserId = context.Message.UserId,
            });
            Console.WriteLine(nameof(PostCreatedEventConsumer) + "- worked");
        }
    }
}
