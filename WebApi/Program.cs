using System.Text.Json;
using System.Text.Json.Serialization;
using CodeYad_Blog.CoreLayer.Services.Categories;
using CodeYad_Blog.CoreLayer.Services.Comments;
using CodeYad_Blog.CoreLayer.Services.FileManager;
using CodeYad_Blog.CoreLayer.Services.Posts;
using CodeYad_Blog.CoreLayer.Services.Users;
using CodeYad_Blog.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure;
using WebApi.Infrastructure.JwtUtil;
using WebApi.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;
services.AddJwtAuthentication(builder.Configuration);

services.AddScoped<IUserService, UserService>();
services.AddScoped<IPostService, PostService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<ICommentService, CommentService>();
services.AddScoped<IFileManager, FileManager>();

services.AddDbContext<BlogContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)))
    .ConfigureApiBehaviorOptions(option =>
    {
        option.InvalidModelStateResponseFactory = context =>
        {
            var result = new ApiResult()
            {
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = ModelStateUtil.GetModelStateErrors(context.ModelState)
                }
            };
            return new BadRequestObjectResult(result);
        };
    });

  
services.AddCors(options =>
{
    options.AddDefaultPolicy(
        conf =>
        {
            conf
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.UseApiCustomExceptionHandler();
app.UseStaticFiles();

app.MapControllers();

app.Run();
