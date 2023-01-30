using Microsoft.EntityFrameworkCore;

namespace bookstore.Entities
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                // Look for any movies.
                if (context.Authors.Any())
                {
                    return;   // DB has been seeded
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Fredrik",
                        Surname = "Backman",
                        DateOfBirth = DateTime.Parse("1981-6-2")
                    },
                    new Author
                    {
                        Name = "Lev",
                        Surname = "Tolstoy",
                        DateOfBirth = DateTime.Parse("1828-9-9")
                    },
                    new Author
                    {
                        Name = "Oscar",
                        Surname = "Wilde",
                        DateOfBirth = DateTime.Parse("1854-10-16")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
