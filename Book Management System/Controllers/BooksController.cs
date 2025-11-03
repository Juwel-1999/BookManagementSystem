using Book_Management_System.Data;
using Book_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _Dbcontext;

        public BooksController(BookDbContext context)
        {
            _Dbcontext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _Dbcontext.Books
                                       .Include(b => b.Category)
                                       .ToListAsync();
            return Ok(books);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<Book>>> GetBooksByCategory(int categoryId)
        {
            var books = await _Dbcontext.Books
                            .Include(b => b.Category)
                            .Where(b => b.CategoryId == categoryId)
                            .ToListAsync();

            if (books == null || books.Count == 0)
                return NotFound();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Book>>> GetBooksById(int id)
        {
            var book = await _Dbcontext.Books
                            .Include(b => b.Category)
                            .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newbook)
        {
            if (newbook is null)
                return BadRequest();

            _Dbcontext.Books.Add(newbook);
            await _Dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooksById), new { id = newbook.Id }, newbook);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book UpdatedBook)
        {
            var book = await _Dbcontext.Books
                           .Include(b => b.Category)
                           .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return BadRequest();

            book.Title = UpdatedBook.Title;
            book.Author = UpdatedBook.Author;
            book.ISBN = UpdatedBook.ISBN;
            book.Price = UpdatedBook.Price;
            book.CategoryId = UpdatedBook.CategoryId;
            book.Category = UpdatedBook.Category;            

            await _Dbcontext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _Dbcontext.Books
                           .Include(b => b.Category)
                           .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return BadRequest();

            _Dbcontext.Remove(book);
            await _Dbcontext.SaveChangesAsync();

            return NoContent();
        }
    }
}
