namespace bookstore.Models;
using bookstore.Entities;

public class OrdersViewModel
{
    public Order Order { get; set; }
    public OrderItem OrderItem { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public Book Book { get; set; }
}
