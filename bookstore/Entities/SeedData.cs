/*using Microsoft.EntityFrameworkCore;

namespace bookstore.Entities
{
    public class SeedData
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                 serviceProvider.GetRequiredService<
                     DbContextOptions<AppDbContext>>()))
            {
                if (context.Roles.Any())
                {
                    return;
                }

                context.Roles.AddRange(

                   new Role
                   {
                       Name = "Admin",

                   },
                    new Role
                    {
                        Name = "Customer",

                    }, new Role
                    {
                        Name = "Librarian",

                    }

                );
                if (context.Authors.Any())
                {
                    return;   
                }
                context.Authors.AddRange(
                    new Author
                    {
                        FullName = "Fredrik Backman",
                        DateOfBirth = DateTime.Parse("1981-6-2")
                    },
                    new Author
                    {
                        FullName = "Lev Tolstoy",
                        DateOfBirth = DateTime.Parse("1828-9-9")
                    },
                    new Author
                    {
                        FullName = "Oscar Wilde",
                        DateOfBirth = DateTime.Parse("1854-10-16")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
*/