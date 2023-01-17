using System.ComponentModel.DataAnnotations;

namespace bookstore.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Book> Books { get; set; }

    }
}
