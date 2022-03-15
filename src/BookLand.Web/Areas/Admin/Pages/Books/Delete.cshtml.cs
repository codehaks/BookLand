using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookLandDbContext _db;

        public DeleteModel(BookLandDbContext db)
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
            _db.Books.Remove(book);

            _db.SaveChanges();
            return RedirectToPage("./index");
        }
    }
}
