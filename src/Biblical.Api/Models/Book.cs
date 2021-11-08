using System;
using System.Linq;

namespace Biblical.Api.Models
{
    public class Book
    {
        public Guid BookId { get; private set; }
        public string Name { get; private set; }

        public Book(string name)
        {
            if (_isValidBookOfTheBible(name) == false)
            {
                throw new Exception("Not a valid book of the bible!");
            }

            Name = name;
        }

        private Book()
        {

        }

        private bool _isValidBookOfTheBible(string name) => new string[2] {
                "Genesis",
                "Exodus"
            }.Contains(name);
    }
}
