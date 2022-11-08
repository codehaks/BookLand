using BookLand.Data;
using BookLand.Models;
using Mapster;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookLand.Application.Books.Commands;

public class CreateBook
{

    public class Command:IRequest<Unit>
    {

        public string Title { get; set; } = default!;

        public int Price { get; set; }

        public string Author { get; set; } = default!;

        public int Year { get; set; } = default;
      
        public int Pages { get; set; } = 1;

        public string Country { get; set; } = default!;

        public string Language { get; set; } = default!;

        public string ImageLink { get; set; } = default!;

        public string Link { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly BookLandDbContext _db;

        public Handler(BookLandDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {

            var book = request.Adapt<Book>();

            _db.Books.Add(book);
            _db.SaveChanges();

            return Unit.Value; 
        }
    }


}
