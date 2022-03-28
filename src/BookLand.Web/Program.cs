using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Admin", "/", "RequireAdminRole");

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

builder.Services.AddIdentity<ApplicationUser,IdentityRole>
    (options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BookLandDbContext>();



var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
