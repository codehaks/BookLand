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
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            if (Book is null)
            {
                return Page();
            }

            var book = _db.Books.Find(Book.Id);

            if (book is null)
            {
                // TODO : Book not found
                return RedirectToPage("./index");
            }

            book.Title = Book.Title;
            book.Author = Book.Author;
            book.Year = Book.Year;
            book.Pages = Book.Pages;

            _db.SaveChanges();
            return RedirectToPage("./index");
        }
    }
}
