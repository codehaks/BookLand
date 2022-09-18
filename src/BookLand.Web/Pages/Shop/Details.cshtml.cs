using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Pages.Shop;

public class DetailsModel : PageModel
{
    private readonly ShopDbContext _db;

    public DetailsModel(ShopDbContext db)
    {
        _db = db;
    }

    public Book? Book { get; set; }

    public void OnGet(int id)
    {
        Book = _db.Books.Find(id);
    }
}
