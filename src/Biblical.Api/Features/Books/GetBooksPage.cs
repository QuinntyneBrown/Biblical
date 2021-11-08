using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Biblical.Api.Extensions;
using Biblical.Api.Core;
using Biblical.Api.Interfaces;
using Biblical.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Biblical.Api.Features
{
    public class GetBooksPage
    {
        public class Request : IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response : ResponseBase
        {
            public int Length { get; set; }
            public List<BookDto> Entities { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IBiblicalDbContext _context;

            public Handler(IBiblicalDbContext context)
                => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from book in _context.Books
                            select book;

                var length = await _context.Books.CountAsync();

                var books = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();

                return new()
                {
                    Length = length,
                    Entities = books
                };
            }

        }
    }
}
