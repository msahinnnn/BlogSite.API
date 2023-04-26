using BlogSite.API.Caching.Abstract;
using BlogSite.API.Models;
using BlogSite.API.Shared.Messages;
using BlogSite.Business.Constants;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using MassTransit;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class CommentChangedEventConsumer : MassTransit.IConsumer<CommentChangedEvent>
    {

        public Task Consume(ConsumeContext<CommentChangedEvent> context)
        {
            throw new NotImplementedException();
        }
      
    }
}
