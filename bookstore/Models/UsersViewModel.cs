using bookstore.Entities;

namespace bookstore.Models
{
    public class UsersViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string StringOfRoles { get; set; } = "Customer";

    }
}
