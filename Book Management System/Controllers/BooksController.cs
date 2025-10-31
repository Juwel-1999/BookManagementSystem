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

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Book>>> GetBooksById(int id)
        {
            var book = await _Dbcontext.Books
                            .Include(b => b.Category)
                            .ToListAsync();

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<List<Book>>> AddBook(int id)
        //{
        //    var book = await _Dbcontext.Books.FindAsync(id);
        //    if (book is null)
        //        return NotFound();

        //    return Ok(book);
        //}
    }
}
