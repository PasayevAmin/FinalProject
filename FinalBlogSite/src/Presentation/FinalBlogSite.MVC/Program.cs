using FinalBlogSite.Persistence.DAL;
using Microsoft.EntityFrameworkCore;
using FinalBlogSite.Persistence.ServiceRegistrations;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using FinalBlogSite.Application.ServiceRegistration;
using System.Configuration;
using FinalBlogSite.MVC.MiddleWears;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});
app.UseMiddleware<GlobalExceptionMiddlewares>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
