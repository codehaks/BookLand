using BookLand.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLand.Data;

public class BookLandDbContext : DbContext
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
