using BookLand.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookLand.Data;

public class BookLandDbContext : IdentityDbContext
{
    public BookLandDbContext(DbContextOptions<BookLandDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }


}
