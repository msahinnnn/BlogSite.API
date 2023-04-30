// See https://aka.ms/new-console-template for more information
using BlogSite.API.Caching.Abstract;
using BlogSite.API.Caching.Concrete;
using BlogSite.API.Caching.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("CONSOLE APP TEST");


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<ICommentCacheService, ICommentCacheService>();
        services.AddScoped<IPostCacheService, PostCacheService>();

        
        services.AddMassTransit(x =>
        {
            //..
            x.AddConsumer<CommentCreatedEventConsumer>();
            x.AddConsumer<CommentUpdatedEventConsumer>();
            x.AddConsumer<CommentDeletedEventConsumer>();
            x.AddConsumer<PostCreatedEventConsumer>();
            x.AddConsumer<PostUpdatedEventConsumer>();
            x.AddConsumer<PostDeletedEventConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("comment-created-event-cache-service", e =>
                {
                    e.ConfigureConsumer<CommentCreatedEventConsumer>(context);
                });
                cfg.ReceiveEndpoint("comment-updated-event-cache-service", e =>
                {
                    e.ConfigureConsumer<CommentUpdatedEventConsumer>(context);
                });
                cfg.ReceiveEndpoint("comment-deleted-event-cache-service", e =>
                {
                    e.ConfigureConsumer<CommentDeletedEventConsumer>(context);
                });
                cfg.ReceiveEndpoint("post-created-event-cache-service", e =>
                {
                    e.ConfigureConsumer<PostCreatedEventConsumer>(context);
                });
                cfg.ReceiveEndpoint("post-updated-event-cache-service", e =>
                {
                    e.ConfigureConsumer<PostUpdatedEventConsumer>(context);
                });
                cfg.ReceiveEndpoint("post-deleted-event-cache-service", e =>
                {
                    e.ConfigureConsumer<PostDeletedEventConsumer>(context);
                });
            });
        });
    }).Build();


await host.RunAsync();






















