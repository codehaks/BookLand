using BookLand.Data;
using BookLand.Models;
using BookLand.Web.Common;
using BookLand.Web.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(UserCreatedEvent).GetTypeInfo().Assembly);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
});

builder.Services.AddRazorPages(options =>
{
    //options.Conventions.AuthorizeAreaFolder("Admin", "/", "RequireAdminRole");

}).AddMvcOptions(options =>
{
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        _ => "The field is required.");
});

builder.Services.AddDbContext<BookLandDbContext>(options =>
{
    options.UseSqlite("Data Source=bookland.sqlite");
    options.LogTo(Console.WriteLine);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>
    (options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BookLandDbContext>().AddDefaultTokenProviders(); ;


builder.Services.AddTransient<IEmailSender, EmailSender>();




var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
