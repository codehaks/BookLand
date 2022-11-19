using BookLand.Application.Books.Queries;
using BookLand.Data;
using BookLand.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookLand.Web.Areas.Admin.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;
    public IndexModel( IMediator mediator)
    {
        _mediator = mediator;
    }

    //public IList<Book> BookList { get; set; }
    public IList<BookViewModel> BookList { get; set; }

    public string Term { get; set; }
    public string SortBy { get; set; }

    [BindProperty]
    public string SelectedLanguage { get; set; }
    public async Task<IActionResult> OnGet(string term = "", string orderBy = "author", string sortBy = "asc", string selectedLanguage = "All")
    {
        var result = await _mediator.Send(new FindAllBooks.Query
        {
            Term = term,
            SortBy=sortBy,
            SelectedLanguage=selectedLanguage,
            OrderBy=orderBy

        });

        BookList = result.Books.Adapt<IList<BookViewModel>>();
        Term = term;
        SortBy = GetSortBy(sortBy);
        SelectedLanguage = selectedLanguage;
        return Page();
    }

    private static string GetSortBy(string sortBy)
    {
        if (sortBy == "asc")
        {
            sortBy = "des";
        }
        else
        {
            sortBy = "asc";
        }

        return sortBy;
    }


}


public class BookViewModel
{
    public int Id { get; set; }

    public Category? Category { get; set; }

    public string Title { get; set; } = default!;

    public int Price { get; set; }

    public string Author { get; set; } = default!;

    public int Year { get; set; } = default;

    public int Pages { get; set; } = 1;

    public string ImageLink { get; set; } = default!;

    public string Link { get; set; } = default!;
}