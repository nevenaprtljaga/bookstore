using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookstore.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }
        /*public List<OrderItem> ListOfOrderItems { get; set; }*/

        public int UserId { get; set; }
    }
}
