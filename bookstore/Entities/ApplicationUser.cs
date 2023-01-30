using Microsoft.AspNetCore.Identity;

namespace bookstore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
