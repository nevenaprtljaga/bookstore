using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookstore.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }
        //public List<Book> ListOfBooks { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
