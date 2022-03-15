using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books
{
    
    public class EditModel : PageModel
    {
        private readonly BookLandDbContext _db;

        public EditModel(BookLandDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book? Book { get; set; }

        public void OnGet(int id)
        {
            Book = _db.Books.Find(id);
        }

        public IActionResult OnPost()
        {
            var book = _db.Books.Find(Book.Id);
            book.Title=Book.Title;

            _db.SaveChanges();
            return RedirectToPage("./index");
        }
    }
}
