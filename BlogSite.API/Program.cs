using BlogSite.API.Mapping;
using BlogSite.API.Services.Abstract;
using BlogSite.API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
