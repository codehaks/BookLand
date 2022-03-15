using BookLand.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<BookLandDbContext>(options =>
{
    options.UseSqlite("Data Source=bookland.sqlite");
    options.LogTo(Console.WriteLine);
});

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
