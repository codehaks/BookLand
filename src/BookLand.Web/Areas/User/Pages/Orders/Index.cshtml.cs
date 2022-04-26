using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.User.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly BookLandDbContext _db;

    public IndexModel(BookLandDbContext db)
    {
        _db = db;
    }

    public IList<Order> OrderList { get; set; }
    public IActionResult OnGet()
    {
        var userName = User?.Identity?.Name;

        if (string.IsNullOrEmpty(userName))
        {
            // retutn not found!
            return RedirectToPage("./notfound");
        }

        OrderList = _db.Orders.Where(o => o.UserName == userName).ToList();
        return Page();
    }
}
