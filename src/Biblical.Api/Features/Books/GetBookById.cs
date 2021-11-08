using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Biblical.Api.Core;
using Biblical.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblical.Api.Features
{
    public class GetBookById
    {
        public class Request : IRequest<Response>
        {
            public Guid BookId { get; set; }
        }

        public class Response : ResponseBase
        {
            public BookDto Book { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IBiblicalDbContext _context;

            public Handler(IBiblicalDbContext context)
                => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new()
                {
                    Book = (await _context.Books.SingleOrDefaultAsync(x => x.BookId == request.BookId)).ToDto()
                };
            }

        }
    }
}
