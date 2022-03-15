using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookLandDbContext _db;

        public CreateModel(BookLandDbContext db)
        {
            _db = db;
        }

        public IActionResult OnPost(string title)
        {
            var book = new Book
            {
                Title = title,
            };

            _db.Books.Add(book);
            _db.SaveChanges();
            return RedirectToPage("./index");
        }
    }
}
