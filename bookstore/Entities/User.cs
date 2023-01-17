using bookstore.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Entities
{
    public class User //: IdentityUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public List<Book> ListOfRentedBooks { get; set; }
        public List<Order> ListOfOrders { get; set; }

    }
}
