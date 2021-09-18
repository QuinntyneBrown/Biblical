using System;
using Biblical.Api.Models;

namespace Biblical.Api.Features
{
    public static class BookExtensions
    {
        public static BookDto ToDto(this Book book)
        {
            return new ()
            {
                BookId = book.BookId
            };
        }
        
    }
}
