using BookLand.Data;
using BookLand.Models;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLand.Application.Books.Queries;

public class FindBook
{
    public class Query:IRequest<Response>
    {
        public int Id { get; set; }
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
            var book= await _db.Books.FindAsync(new object[] {query.Id },cancellationToken);
            // mange null 
            var response = book.Adapt<Response>();
            return response;
        }
    }

    public class Response
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
}
