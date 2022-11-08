using MediatR;

namespace BookLand.Application.Books.Commands;

public class CreateBook
{
    // Req
    // Handler
    // Res

    public class Request:IRequest<Response>
    {
        public int Number { get; set; }
    }

    public class Response
    {
        
    }

    public class Handler : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            request.Number= request.Number + 1;

            return new Response();
        }
    }


}
