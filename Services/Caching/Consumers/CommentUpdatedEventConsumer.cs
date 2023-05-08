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
    public class CommentUpdatedEventConsumer : MassTransit.IConsumer<CommentUpdatedEvent>
    {
        private ICommentCacheService _cacheService;

        public CommentUpdatedEventConsumer(ICommentCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<CommentUpdatedEvent> context)
        {
            Console.WriteLine(context.Message);
            var x = await _cacheService.SaveOrUpdateAsync(new Comment()
            {
                Id = context.Message.Id,
                Content = context.Message.Content,
                UserId = context.Message.UserId,
                PostId = context.Message.PostId,
                CreateTime = context.Message.CreateTime,
            });
            Console.WriteLine(nameof(CommentUpdatedEventConsumer) + "- worked");
        }
    }
}
