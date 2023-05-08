using BlogSite.API;
using BlogSite.API.Controllers;
using BlogSite.API.Extensions;
using BlogSite.API.Mapping;
using BlogSite.API.Models;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Concrete;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using BlogSite.DataAccsess.Concrete.Dapper;
using BlogSite.DataAccsess.EntitiyFramework.ApplicationContext;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.MSSqlServer;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Security;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Claims;
using System.ServiceProcess;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//test commit
builder.Services.AddHttpContextAccessor();

var con = builder.Configuration["ConnectionStrings:MsSqlConnection"];

builder.Services.AddDbContext<BlogSiteDbContext>(opt => opt.UseSqlServer(con));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
    .WriteTo.File("logs/logs.txt")
        .WriteTo.MSSqlServer(
            connectionString: builder.Configuration["ConnectionStrings:MsSqlSerilogConnection"],
            sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true }
        )
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
    {
        IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
        AutoRegisterTemplate = true,
        NumberOfShards = 2,
        NumberOfReplicas = 1
    })
    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
    .ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(CommentProfile));
builder.Services.AddAutoMapper(typeof(PostProfile));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped <IAuthService, AuthService>();
builder.Services.AddScoped<ICommentCacheService, CommentCacheService>();
builder.Services.AddScoped<IPostCacheService, PostCacheService>();

builder.Services.AddScoped<ITokenHandler, BlogSite.Business.Authentication.TokenHandler>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var redisConnection = builder.Configuration["ConnectionStrings:RedisConnection"];


var multiplexer = ConnectionMultiplexer.Connect(redisConnection);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);


builder.Services.AddScoped<IDatabase>(cfg =>
{
    IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(redisConnection);
    return multiplexer.GetDatabase();
});



builder.Services.AddOptions<RabbitMqTransportOptions>()
        .Configure(options =>
        {
            options.Port = 5672;
            options.UseSsl = false;
            options.Host = "rabbitmq";

        });

builder.Services.AddMassTransit(x =>
{
   
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ConfigureEndpoints(context);

    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

