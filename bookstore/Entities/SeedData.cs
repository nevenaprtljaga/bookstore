using Microsoft.EntityFrameworkCore;

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
                /*    if (context.Roles.Any())
                    {
                        return;
                    }
    */
                context.Roles.AddRange(

                    new Role
                    {
                        Name = "Proba",

                    }/*,
                     new Role
                     {
                         Name = "Customer",

                     }, new Role
                     {
                         Name = "Librarian",

                     }*/

                );
                context.SaveChanges();
            }
        }
    }
}
