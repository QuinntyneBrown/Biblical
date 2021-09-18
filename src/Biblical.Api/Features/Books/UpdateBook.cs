using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Biblical.Api.Core;
using Biblical.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblical.Api.Features
{
    public class UpdateBook
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Book).NotNull();
                RuleFor(request => request.Book).SetValidator(new BookValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public BookDto Book { get; set; }
        }

        public class Response: ResponseBase
        {
            public BookDto Book { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IBiblicalDbContext _context;
        
            public Handler(IBiblicalDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var book = await _context.Books.SingleAsync(x => x.BookId == request.Book.BookId);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Book = book.ToDto()
                };
            }
            
        }
    }
}
