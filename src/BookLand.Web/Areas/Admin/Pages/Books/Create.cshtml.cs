using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookLandDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(BookLandDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            Book = new Book
            {
                Year = 0,
                Pages = 1
            };
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Book? Book { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var fileName= Book.Title.ToLower().Replace(" ", " - ") + ".jpg";
            if (ImageFile!=null)
            {
                var path = _webHostEnvironment.WebRootPath + @"\images\" + fileName;
                
                using var stream = System.IO.File.Create(path);
                stream.Position = 0;
                await ImageFile.CopyToAsync(stream);
                // Copy to images folder
            }
            Book.ImageLink = "images/" + fileName;

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
