using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _db;
        

        public IndexModel(ShopDbContext db)
        {
            _db = db;

        }

        public List<Book> BookList { get; set; }
        public async void OnGet()
        {
            BookList=_db.Books.Take(20).ToList();
        }
    }
}
