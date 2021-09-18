using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Biblical.Api.Core;
using Biblical.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblical.Api.Features
{
    public class GetBooks
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<BookDto> Books { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IBiblicalDbContext _context;
        
            public Handler(IBiblicalDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Books = await _context.Books.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
