// See https://aka.ms/new-console-template for more information
using BlogSite.API.Caching.Abstract;
using BlogSite.API.Caching.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("CONSOLE APP TEST");


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddScoped<ICommentCacheService, ICommentCacheService>();
        services.AddScoped<IPostCacheService, PostCacheService>();
    }).Build();

























