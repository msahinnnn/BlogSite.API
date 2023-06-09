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
    public class CommentDeletedEventConsumer : MassTransit.IConsumer<CommentDeletedEvent>
    {
        private ICommentService _commentService;

        public CommentDeletedEventConsumer(ICommentService commentService)
        {
            _commentService = commentService;
        }


        public async Task Consume(ConsumeContext<CommentDeletedEvent> context)
        {
            Console.WriteLine(context.Message);
            var x = await _commentService.DeleteAsync(context.Message.Id);
            Console.WriteLine(nameof(CommentDeletedEventConsumer) + "- worked");

        }
    }
}
