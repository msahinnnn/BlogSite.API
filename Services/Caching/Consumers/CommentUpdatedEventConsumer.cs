using BlogSite.API.Caching.Abstract;
using BlogSite.API.Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class CommentUpdatedEventConsumer : MassTransit.IConsumer<CommentUpdatedEvent>
    {
        //private ICommentCacheService _cacheService;

        //public CommentUpdatedEventConsumer(ICommentCacheService cacheService)
        //{
        //    _cacheService = cacheService;
        //}

        public async Task Consume(ConsumeContext<CommentUpdatedEvent> context)
        {
            Console.WriteLine(context.Message);
            //await _cacheService.SaveOrUpdateAsync(context.Message);
            Console.WriteLine(nameof(CommentUpdatedEventConsumer) + "- worked");
        }
    }
}
