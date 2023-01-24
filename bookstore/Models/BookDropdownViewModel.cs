using bookstore.Entities;
namespace bookstore.Models

{
    public class BookDropdownViewModel
    {
        public List<Author> Authors { get; set; }
        public List<BookGenre> BookGenres { get; set; }

        public BookDropdownViewModel() { 
            Authors = new List<Author>();
            BookGenres = new List<BookGenre>();
        }
    }
}