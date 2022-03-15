using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly BookLandDbContext _db;

        public DetailsModel(BookLandDbContext db)
        {
            _db = db;
        }

        public Book? Book { get; set; }

        public void OnGet(int id)
        {
            Book = _db.Books.Find(id);
        }
    }
}
