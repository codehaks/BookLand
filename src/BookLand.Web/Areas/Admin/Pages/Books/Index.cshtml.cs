using BookLand.Data;
using BookLand.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

    [BindProperty]
    public string SelectedLanguage { get; set; }
    public void OnGet(string term = "", string orderBy = "author", string sortBy = "asc",string selectedLanguage="All")
    {
        IQueryable<Book> booksQuery = _db.Books.Include(b=>b.Category);

        booksQuery = OrdeBooks(orderBy, sortBy, booksQuery);
        sortBy = GetSortBy(sortBy);
        SortBy = sortBy;

        if (string.IsNullOrEmpty(term) == false)
        {
            booksQuery = booksQuery
                .Where(b => b.Title.ToLower()
                .Contains(term.ToLower()));
        }

        if (string.IsNullOrEmpty(selectedLanguage) ==false && selectedLanguage != "All")
        {
            booksQuery=booksQuery.Where(b=>b.Language== selectedLanguage);
        }

        BookList = booksQuery.ToList();
        Term = term;
        SelectedLanguage = selectedLanguage;

    }

    private static IQueryable<Book> OrdeBooks(string orderBy, string sortBy, IQueryable<Book> booksQuery)
    {
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

        return booksQuery;
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
