using AutoMapper;
using Caching.Abstract;
using Caching.Entities;
using Caching.Messages;
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
            Console.WriteLine(context.Message);
            var x = await _cacheService.SaveOrUpdateAsync(new Comment()
            {
                Id = context.Message.Id,
                CreateTime = context.Message.CreateTime,
                Content = context.Message.Content,
                PostId = context.Message.PostId,
                UserId = context.Message.UserId,
            });
            Console.WriteLine(nameof(CommentCreatedEventConsumer) + "- worked");
        }
    }
}
