using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteka.DatabContext;
using Biblioteka.Model;
using Biblioteka.Requests;



[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BiblioApiDB _context;

    public BooksController(BiblioApiDB context)
    {
        _context = context;
    }

    // GET: api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Book.ToListAsync();
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Book.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Book.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBook", new { id = book.Id_Book }, book);
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id_Book)
        {
            return BadRequest();
        }

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Book.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Book.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.Id_Book == id);
    }
}

































//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Biblioteka.DatabContext;
//using Biblioteka.Model;
//using Biblioteka.Requests;



//namespace Biblioteka.Controllers
//{


//    // BooksController.cs
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BooksController : ControllerBase
//    {
//        private readonly BiblioApiDB _context;

//        public BooksController(BiblioApiDB context)
//        {
//            _context = context;
//        }

//        // GET: api/books
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
//        {
//            return await _context.Book.ToListAsync();
//        }

//        // GET: api/books/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Book>> GetBook(int id)
//        {
//            var book = await _context.Book.FindAsync(id);

//            if (book == null)
//            {
//                return NotFound();
//            }

//            return book;
//        }

//        // POST: api/books
//        [HttpPost]
//        public async Task<ActionResult<Book>> PostBook(Book book)
//        {
//            _context.Book.Add(book);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetBook), new { id = book.Id_Book }, book);
//        }

//        // PUT: api/books/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutBook(int id, Book book)
//        {
//            if (id != book.Id_Book)
//            {
//                return BadRequest();
//            }

//            _context.Entry(book).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // DELETE: api/books/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBook(int id)
//        {
//            var book = await _context.Book.FindAsync(id);
//            if (book == null)
//            {
//                return NotFound();
//            }

//            _context.Book.Remove(book);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // GET: api/books/genre/1
//        [HttpGet("genre/{genreId}")]
//        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByGenre(int genreId)
//        {
//            return await _context.Book
//                .Where(b => b.GenreID == genreId)
//                .ToListAsync();
//        }

//        // GET: api/books/search?author=John+Doe&title=The+Catcher+in+the+Rye
//        [HttpGet("search")]
//        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(string author, string title)
//        {
//            var query = _context.Book;

//            if (!string.IsNullOrEmpty(author))
//            {
//                query = (DbSet<Book>)query.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
//            }

//            if (!string.IsNullOrEmpty(title))
//            {
//                query = (DbSet<Book>)query.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
//            }

//            return await query.ToListAsync();
//        }


//    }
//}




