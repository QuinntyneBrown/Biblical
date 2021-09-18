using System;
using System.Linq;

namespace Biblical.Api.Models
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }

        public Book(string name)
        {
            if(_isValidBookOfTheBible(name) == false)
            {
                throw new Exception("Not a valid book of the bible!");
            }

            Name = name;
        }

        private bool _isValidBookOfTheBible(string name)
        {
            string[] booksOfTheBible = new string[2] {
                "Genesis",
                "Exodus"
            };

            if(booksOfTheBible.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
