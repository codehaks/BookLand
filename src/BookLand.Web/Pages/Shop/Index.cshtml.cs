using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly BookLandDbContext _db;

        public IndexModel(BookLandDbContext db)
        {
            _db = db;
        }

        public List<Book> BookList { get; set; }

        public void OnGet()
        {
            BookList=_db.Books.Take(20).ToList();
        }
    }
}
