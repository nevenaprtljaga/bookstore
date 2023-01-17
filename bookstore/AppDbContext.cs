using bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(u =>
            {
                u.ToTable("User");
                u.HasKey("Id");
                //u.HasMany<Order>().WithOne().HasForeignKey(o => o.UserId).IsRequired();
                //u.HasMany<Book>().WithOne().HasForeignKey(b => b.UserId).IsRequired(false);
            });

            builder.Entity<Order>(o =>
            {
                o.ToTable("Order");
                o.HasKey("Id");
                //o.HasMany<Book>().WithOne().HasForeignKey(b => b.OrderId).IsRequired(false);
            });

            builder.Entity<Author>(a =>
            {
                a.ToTable("Author");
                a.HasKey("Id");
                //a.HasMany<Book>().WithOne().HasForeignKey(b => b.AuthorId).IsRequired();
            });

            builder.Entity<Book>(b =>
            {
                b.ToTable("Book");
                b.HasKey("Id");
            });

            builder.Entity<Role>(r =>
            {
                r.ToTable("Role");
                r.HasKey("Id");
                //r.HasMany<User>().WithOne().HasForeignKey(u => u.RoleId).IsRequired();
            });

            builder.Entity<BookGenre>(bg =>
            {
                bg.ToTable("BookGenre");
                bg.HasKey("Id");
                //bg.HasMany<Book>().WithOne().HasForeignKey(b => b.BookGenreId).IsRequired();
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
    }
}
