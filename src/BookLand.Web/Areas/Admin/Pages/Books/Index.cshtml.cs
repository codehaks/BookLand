using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Areas.Admin.Pages.Books;

public class IndexModel : PageModel
{
    private readonly BookLandDbContext _db;

    public IndexModel(BookLandDbContext db)
    {
        _db = db;
    }

    public IList<Book> BookList { get; set; }

    public string Term { get; set; }
    public string SortBy { get; set; }
    public void OnGet(string term = "", string orderBy = "author", string sortBy = "asc")
    {
        IQueryable<Book> booksQuery = _db.Books;

        if (orderBy == "year")
        {
            if (sortBy == "asc")
            {
                booksQuery = booksQuery.OrderBy(b => b.Year);
            }
            else
            {
                booksQuery = booksQuery.OrderByDescending(b => b.Year);
            }
        }

        if (orderBy == "author")
        {

            if (sortBy == "asc")
            {
                booksQuery = booksQuery.OrderBy(b => b.Author);
            }
            else
            {
                booksQuery = booksQuery.OrderByDescending(b => b.Author);
            }
        }

        sortBy = GetSortBy(sortBy);

        SortBy = sortBy;


        if (string.IsNullOrEmpty(term) == false)
        {
            booksQuery = booksQuery
                .Where(b => b.Title.ToLower()
                .Contains(term.ToLower()));
        }

        BookList = booksQuery.ToList();
        Term = term;

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
