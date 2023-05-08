using BlogSite.Messages.Events;
using Caching.Abstract;
using Caching.Entities;
using MassTransit;

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
            var x = await _cacheService.SaveOrUpdateAsync(new Comment()
            {
                Id = context.Message.Id,
                CreateTime = context.Message.CreateTime,
                Content = context.Message.Content,
                PostId = context.Message.PostId,
                UserId = context.Message.UserId,
            });
            Console.WriteLine(x);
        }
    }
}
