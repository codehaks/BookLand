using BookLand.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        _ => "The field is required.");
});

builder.Services.AddDbContext<BookLandDbContext>(options =>
{
    options.UseSqlite("Data Source=bookland.sqlite");
    options.LogTo(Console.WriteLine);
});

builder.Services.AddDefaultIdentity<IdentityUser>
    (options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BookLandDbContext>();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
