using CodeYad_Blog.CoreLayer.Services;
using CodeYad_Blog.CoreLayer.Services.Categories;
using CodeYad_Blog.CoreLayer.Services.Comments;
using CodeYad_Blog.CoreLayer.Services.FileManager;
using CodeYad_Blog.CoreLayer.Services.Posts;
using CodeYad_Blog.CoreLayer.Services.Users;
using CodeYad_Blog.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddRazorPages()
                .AddRazorRuntimeCompilation();

services.AddControllersWithViews();
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
services.AddAuthorization(option =>
{
    option.AddPolicy("AdminPolicy", builder =>
    {
        builder.RequireRole("Admin");
    });
});

services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/Auth/Login";
    option.LogoutPath = "/Auth/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
    option.AccessDeniedPath = "/";
});

var app = builder.Build();
if (!builder.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ErrorHandler/500");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
                    name: "Default",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    ); ;
app.MapRazorPages();

app.Run();