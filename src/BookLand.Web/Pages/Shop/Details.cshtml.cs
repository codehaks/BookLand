using BookLand.Application.Books.Queries;
using BookLand.Data;
using BookLand.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Pages.Shop;

public class DetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public BookDetailsModel Book { get; set; }

    public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new FindBook.Query { Id = id },cancellationToken);
        Book=response.Adapt<BookDetailsModel>();
        return Page();
    }
}

public class BookDetailsModel
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public int Price { get; set; }

    public string Author { get; set; } = default!;

    public int Year { get; set; } = default;

    public int Pages { get; set; } = 1;

    public string ImageLink { get; set; } = default!;

    public string Link { get; set; } = default!;
}
