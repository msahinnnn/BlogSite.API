using BlogSite.Business.Constants;
using BlogSite.Messages.Events;
using Caching.Abstract;
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
        private IPostService _postService;

        public PostDeletedEventConsumer(IPostService postService)
        {
            _postService = postService;
        }


        public async Task Consume(ConsumeContext<PostDeletedEvent> context)
        {
            Console.WriteLine(context.Message);
            var x = await _postService.DeleteAsync(context.Message.Id);
            Console.WriteLine(nameof(PostDeletedEventConsumer) + "- worked");

        }
    }
}
