using BookLand.Data;
using BookLand.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookLand.Application.Books.Queries;

public class FindAllBooks
{
    public class Query : IRequest<Response>
    {
        public int Id { get; set; }
        public string Term { get; set; }

        public string SortBy { get; set; }

        public string SelectedLanguage { get; set; }

        public string OrderBy { get; set; }
    }

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly BookLandDbContext _db;

        public Handler(BookLandDbContext db)
        {
            _db = db;
        }

        public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
        {
            IQueryable<Book> booksQuery = _db.Books.Include(b => b.Category);
            booksQuery = OrderBooks(query.OrderBy, query.SortBy, booksQuery);
            
            if (string.IsNullOrEmpty(query.Term) == false)
            {
                booksQuery = booksQuery
                    .Where(b => b.Title.ToLower()
                    .Contains(query.Term.ToLower()));
            }

            if (string.IsNullOrEmpty(query.SelectedLanguage) == false && query.SelectedLanguage != "All")
            {
                booksQuery = booksQuery.Where(b => b.Language == query.SelectedLanguage);
            }

            var BookList = booksQuery
                .ProjectToType<BookOutput>()
                .ToList();

            var response = new Response
            {
                Books = BookList
            };
            return response;
        }

        private static IQueryable<Book> OrderBooks(string orderBy, string sortBy, IQueryable<Book> booksQuery)
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
    }

    public class Response
    {
        public IList<BookOutput> Books { get; set; }
    }
}


public class BookOutput
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