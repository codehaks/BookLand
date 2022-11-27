using BookLand.Application;
using BookLand.Application.Books.Commands;
using BookLand.Data;
using BookLand.Models;
using FluentAssertions.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((builder, logger) =>
{
    logger.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

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
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>
    (options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BookLandDbContext>().AddDefaultTokenProviders(); ;


builder.Services.AddTransient<IMyEmailSender, EmailSender>();
builder.Services.AddMediatR(typeof(ApplicationUser));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));



var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
