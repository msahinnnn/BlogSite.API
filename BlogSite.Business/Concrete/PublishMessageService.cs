using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSite.API.Shared.Messages;
using BlogSite.Business.Abstract;

namespace BlogSite.Business.Concrete
{
    //public class PublishMessageService : BackgroundService
    //{
    //    readonly IPublishEndpoint _publishEndpoint;

    //    public PublishMessageService(IPublishEndpoint publishEndpoint)
    //    {
    //        _publishEndpoint = publishEndpoint;
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        await _publishEndpoint.Publish("safsfd");
    //    }

        
    //}
}
