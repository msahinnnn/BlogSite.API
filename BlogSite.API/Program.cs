using BlogSite.API.Controllers;
using BlogSite.API.Extensions;
using BlogSite.API.Mapping;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.DataAccsess.EntitiyFramework.ApplicationContext;
using BlogSite.DataAccsess.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.MSSqlServer;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureLogs();

//var connectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
var con = builder.Configuration["ConnectionStrings:MsSqlConnection"];

builder.Services.AddDbContext<BlogSiteDbContext>(opt => opt.UseSqlServer(con));

//Logger log = new LoggerConfiguration()
//    .WriteTo.File("logs/logs.txt")
//    .WriteTo.MSSqlServer(
//        connectionString: builder.Configuration["ConnectionStrings:MsSqlSerilogConnection"],
//        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true }
//    )
//    .Enrich.FromLogContext()
//    .MinimumLevel.Information()
//    .CreateLogger();
builder.Host.UseSerilog();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(CommentProfile));
builder.Services.AddAutoMapper(typeof(PostProfile));

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IPostService, PostService>();
builder.Services.AddSingleton<ICommentService, CommentService>();
builder.Services.AddSingleton<IRedisService, RedisService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();

//var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");
var redisConnection = builder.Configuration["ConnectionStrings:RedisConnection"];
builder.Services.AddStackExchangeRedisCache(options => 
    options.Configuration = redisConnection
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.ExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureLogs(){
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    //var configuration = new ConfigurationBuilder()
    //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //    .Build();

    Log.Logger = new LoggerConfiguration()
        .WriteTo.File("logs/logs.txt")
        .WriteTo.MSSqlServer(
            connectionString: builder.Configuration["ConnectionStrings:MsSqlSerilogConnection"],
            sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true }
        )
        .WriteTo.Elasticsearch(ConfigureELS(env))
        .Enrich.FromLogContext()
        .MinimumLevel.Information()
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureELS(string env)
{
    return new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".","-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}