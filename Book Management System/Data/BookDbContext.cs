using Book_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Data
{
    public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    ISBN = "9780743273565",
                    CategoryId = 10,
                    Price = 2000,
                    Category = null
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    ISBN = "9780061120084",
                    CategoryId = 20,
                    Price = 2500,
                    Category = null
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    ISBN = "9780451524935",
                    CategoryId = 10,
                    Price = 1800,
                    Category = null
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 10,
                    Name = "Fiction",
                    Books = null
                },
                new Category
                {
                    Id = 20,
                    Name = "Classic",
                    Books = null
                },
                new Category
                {
                    Id = 30,
                    Name = "Science Fiction",
                    Books = null
                }
            );
        }
    }
}
