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
            Book=new Book
            {
                Year=0,
                Pages=1
            };
        }

        [BindProperty]
        public Book? Book { get; set; }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid==false)
            {
                return Page();
            }
            if (Book is null)
            {
                return Page();
            }

            _db.Books.Add(Book);
            _db.SaveChanges();
            return RedirectToPage("./index");
        }
    }
}
