using BlogSite.Messages.Events;
using Caching.Abstract;
using Caching.Entities;
using MassTransit;

namespace BlogSite.API.Caching.Consumers
{
    public class PostCreatedEventConsumer : MassTransit.IConsumer<PostCreatedEvent>
    {
        private IPostService _postService;

        public PostCreatedEventConsumer(IPostService postService)
        {
            _postService = postService;
        }

        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            Console.WriteLine(context.Message);
            var x = await _postService.SaveOrUpdateAsync(new Post()
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
