using BookLand.Application.Orders.Notifications;
using BookLand.Data;
using BookLand.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Pages.Shop;

public class OrderModel : PageModel
{
    private readonly BookLandDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;
    public OrderModel(BookLandDbContext db, UserManager<ApplicationUser> userManager, IMediator mediator)
    {
        _db = db;
        _userManager = userManager;
        _mediator = mediator;
    }

    public Book? Book { get; set; }

    [BindProperty]
    public int BookId { get; set; }

    public string ShippingAddress { get; set; }
    public int Quantity { get; set; }

    public async void OnGet(int id)
    {
        Book = await _db.Books.FindAsync(id);
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
            ShippingAddress=ShippingAddress,
            Quantity=Quantity,
            TimeCreated = DateTime.Now
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        await _mediator.Publish(new OrderCreatedNotification
        {
            BookId = book.Id,
            BookName = book.Title,
            UserName = user.UserName
        });

        TempData["success"] = "Your order confirmed.";

        // Send email
        // Notify Admin
        // Notify Accounting
        // Notify Warehouse

        return RedirectToPage("/orders/index", new { area = "user" });
    }
}
