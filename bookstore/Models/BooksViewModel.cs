using bookstore.Entities;

namespace bookstore.Models
{
    public class BooksViewModel
    {
        public Book Book { get; set; }
        public Author Author { get; set; }
        public BookGenre BookGenre{ get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}