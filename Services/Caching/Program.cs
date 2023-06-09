// See https://aka.ms/new-console-template for more information
using BlogSite.API.Caching.Consumers;
using Caching.Abstract;
using Caching.Concrete;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;


Console.WriteLine("CONSOLE APP TEST");

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ICommentCacheService, CommentCacheService>();
        services.AddSingleton<IPostCacheService, PostCacheService>();
        services.AddSingleton<IPostRepository, PostRepository>();
        services.AddSingleton<ICommentRepository, CommentRepository>();
        services.AddSingleton<ICommentService, CommentService>();
        services.AddSingleton<IPostService, PostService>();

        //IConfiguration _configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        var multiplexer = ConnectionMultiplexer.Connect("app_redis, abortConnect=false");
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);


        services.AddScoped<IDatabase>(cfg =>
        {
            IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("app_redis, abortConnect=false");
            return multiplexer.GetDatabase();
        });



        services.AddOptions<RabbitMqTransportOptions>()
        .Configure(options =>
        {
            options.Port = 5672;
            options.UseSsl = false;
            options.Host = "rabbitmq";
        });

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
                cfg.Host("rabbitmq", "/", host =>
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

                cfg.ConfigureEndpoints(context);
            });
        });
    }).Build();


await host.RunAsync();






















