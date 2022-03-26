using CodeYad_Blog.CoreLayer.Services;
using CodeYad_Blog.CoreLayer.Services.Categories;
using CodeYad_Blog.CoreLayer.Services.Comments;
using CodeYad_Blog.CoreLayer.Services.FileManager;
using CodeYad_Blog.CoreLayer.Services.Posts;
using CodeYad_Blog.CoreLayer.Services.Users;
using CodeYad_Blog.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddScoped<IUserService, UserService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddTransient<IPostService, PostService>();
services.AddTransient<IFileManager, FileManager>();
services.AddTransient<ICommentService, CommentService>();
services.AddTransient<IMainPageService, MainPageService>();
services.AddDbContext<BlogContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
services.AddCors(option =>
{
    option.AddPolicy("Default_CORS", o =>
    {
        o.AllowAnyHeader();
        o.AllowAnyMethod();
        o.WithOrigins("http://localhost:8080/",
            "http://www.contoso.com");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors("Default_CORS");
app.UseAuthorization();

app.MapControllers();

app.Run();
