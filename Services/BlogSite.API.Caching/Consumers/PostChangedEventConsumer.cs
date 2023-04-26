using BlogSite.API.Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class PostChangedEventConsumer : IConsumer<PostChangedEvent>
    {
        public Task Consume(ConsumeContext<PostChangedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
