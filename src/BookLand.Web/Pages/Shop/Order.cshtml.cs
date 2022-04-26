using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Pages.Shop;

public class OrderModel : PageModel
{
    private readonly BookLandDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public OrderModel(BookLandDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public Book? Book { get; set; }

    [BindProperty]
    public int BookId { get; set; }

    public string Address { get; set; }

    public async void OnGet(int id)
    {
        var userName = User?.Identity?.Name;
        // if user not found do ...


        var user = await _userManager.FindByNameAsync(userName);
        Address = user.Address;


        Book = _db.Books.Find(id);
        BookId = id;
    }

    public async Task<IActionResult> OnPost()
    {
        var book = _db.Books.Find(BookId);
        var userName = User?.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
 
        var order = new Order
        {
            Amount = book.Price,
            BookId = book.Id,
            UserId = user.Id,
            UserName = userName,
            TimeCreated = DateTime.Now
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return RedirectToPage("./index");
    }
}
