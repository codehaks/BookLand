using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books;

public class IndexModel : PageModel
{
    private readonly BookLandDbContext _db;

    public IndexModel(BookLandDbContext db)
    {
        _db = db;
    }

    public IList<Book> BookList { get; set; }
    public void OnGet()
    {
        BookList = _db.Books.ToList();

    }
}
