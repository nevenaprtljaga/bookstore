using bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set;}
        public DbSet<Role> Roles { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(b =>
            {
                b.ToTable("User");
                b.HasKey("Id");
                b.HasMany<Order>().WithOne().HasForeignKey(o => o.UserId).IsRequired();
            });

            builder.Entity<Order>(o =>
            {
                o.ToTable("Order");
                o.HasKey("Id");
                
            });

            builder.Entity<Author>(a =>
            {
                a.ToTable("Author");
                a.HasKey("Id");
                a.HasMany<Book>().WithOne().HasForeignKey(b => b.AuthorId).IsRequired();
            });

            builder.Entity<Book>(b =>
            {
                b.ToTable("Book");
                b.HasKey("ISBN");
               // b.HasMany<Book>().WithMany(b => b.Orders).UsingEntity(i => i.ToTable("BookItem"));
            });

            builder.Entity<Role>(r =>
            {
                r.ToTable("Role");
                r.HasKey("Id");
                r.HasMany<User>().WithOne().HasForeignKey(u => u.RoleId).IsRequired();
            });

            builder.Entity<BookGenre>(bg =>
            {
                bg.ToTable("BookGenre");
                bg.HasKey("Id");
                bg.HasMany<Book>().WithOne().HasForeignKey(b => b.BookGenreId).IsRequired();
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
