using BookLand.Data;
using BookLand.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books;

public class EditModel : PageModel
{
    private readonly BookLandDbContext _db;

    public EditModel(BookLandDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public BookInputModel BookInputModel { get; set; }

    public void OnGet(int id)
    {
        var book = _db.Books.Find(id);
        Id = id;
        BookInputModel = book.Adapt<BookInputModel>();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        if (BookInputModel is null)
        {
            return Page();
        }

        var book = _db.Books.Find(Id);

        if (book is null)
        {
            // TODO : Book not found
            return RedirectToPage("./index");
        }

        book.Title = BookInputModel.Title;
        book.Author = BookInputModel.Author;
        book.Year = BookInputModel.Year;
        book.Pages = BookInputModel.Pages;
        book.Price = BookInputModel.Price;

        _db.SaveChanges();
        return RedirectToPage("./index");
    }
}
