using BlogSite.Messages.Events;
using Caching.Abstract;
using Caching.Entities;
using MassTransit;

namespace BlogSite.API.Caching.Consumers
{
    public class CommentCreatedEventConsumer : MassTransit.IConsumer<CommentCreatedEvent>
    {
        private ICommentService _commentService;

        public CommentCreatedEventConsumer(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task Consume(ConsumeContext<CommentCreatedEvent> context)
        {
            var x = await _commentService.SaveOrUpdateAsync(new Comment()
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
