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
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var con = builder.Configuration.GetConnectionString("MsSqlConnection");

//var server = builder.Configuration["DbServer"] ?? "localhost";
//var port = builder.Configuration["DbPort"] ?? "1433";
//var user = builder.Configuration["DbUser"] ?? "SA";
//var password = builder.Configuration["Password"] ?? "mrMehmet123#";
//var database = builder.Configuration["Database"] ?? "BlogSiteAppDB";
////var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};";

var connectionString = builder.Configuration.GetConnectionString("MsSqlConnection");

builder.Services.AddDbContext<BlogSiteDbContext>(opt => opt.UseSqlServer(connectionString));



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

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentCacheService, CommentCacheService>();

//builder.Services.AddSingleton<CacheService>(sp =>
//{
//    return new CacheService("localhost:1920,connectTimeout=30000,abortConnect=false,responseTimeout=30000,asyncTimeout=30000,syncTimeout=30000");
//});
//builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:1920,connectTimeout=30000,abortConnect=false,responseTimeout=30000,asyncTimeout=30000,syncTimeout=30000");


builder.Services.AddSingleton<CacheService>(sp =>
{
    return new CacheService("app_redis:1920,abortConnect=false");
});
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "app_redis,abortConnect=false");


//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//ThreadPool.SetMinThreads(1024, 100);
//builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
//    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"))
//);

//ThreadPool.GetMinThreads(out var workerThreads, out var completionPortThreads);

//Console.WriteLine($"Worker Threads = {workerThreads} - Completion Port Threads = {completionPortThreads}");

//ThreadPool.SetMinThreads(workerThreads * 10, completionPortThreads);

//ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);

//Console.WriteLine($"Worker Threads = {workerThreads} - Completion Port Threads = {completionPortThreads}");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//DatabaseManagementService.MigrationInitialisation(app);
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
