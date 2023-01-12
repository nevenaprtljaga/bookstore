using bookstore.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookstore.Entities
{
    public class Book
    {
        public int ISBN { get; set; }

        public string Title { get; set; }
        public int Price { get; set;  }
        public int YearOfPublication { get; set; }
        public int AuthorId { get; set; }
        public int BookGenreId { get; set; }


    }
}
