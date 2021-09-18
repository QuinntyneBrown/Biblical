using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Biblical.Api.Models;
using Biblical.Api.Core;
using Biblical.Api.Interfaces;

namespace Biblical.Api.Features
{
    public class CreateBook
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
                Response response = default;

                try
                { 
                    var book = new Book(request.Book.Name);

                    _context.Books.Add(book);

                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch
                {
                    response = new()
                    {

                        Errors = new()
                        {
                            $"Invalid Book Name. {request.Book.Name} is not a valid book of the Bible."
                        }
                    };

                }
                finally
                {

                }

                return response;
            }


            
        }
    }
}
