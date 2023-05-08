using BlogSite.Business.Constants;
using Caching.Abstract;
using MassTransit;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class CommentDeletedEventConsumer : MassTransit.IConsumer<CommentDeletedEvent>
    {
        private ICommentCacheService _cacheService;

        public CommentDeletedEventConsumer(ICommentCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<CommentDeletedEvent> context)
        {
            Console.WriteLine(context.Message);
            var x = await _cacheService.DeleteAsync(CommentCacheKeys.CommentKey, context.Message.Id);
            Console.WriteLine(nameof(CommentDeletedEventConsumer) + "- worked");

        }
    }
}
