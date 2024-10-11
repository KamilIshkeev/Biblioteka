using Microsoft.AspNetCore.Mvc;
using Biblioteka.Interfaces;

using Biblioteka.Model;
using static Biblioteka.Services.BookService;

namespace Biblioteka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks1([FromQuery] BookSearchFilter filter, [FromQuery] PaginationParams pagination) => await _bookService.GetBooks1Async(filter,  pagination);

        [HttpGet]
        public async Task<IActionResult> GetBooks() => await _bookService.GetBooksAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id) => await _bookService.GetBookAsync(id);

        [HttpPost]
        public async Task<IActionResult> PostBook(Book book) => await _bookService.PostBookAsync(book);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book) => await _bookService.PutBookAsync(id, book);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id) => await _bookService.DeleteBookAsync(id);

        [HttpGet("{id}/availability")]
        public async Task<IActionResult> GetAvailableCopies(int id) => await _bookService.GetAvailableCopiesAsync(id);
    }
}







//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Biblioteka.DatabContext;
//using Biblioteka.Model;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
////using static BooksController.PaginationParams;

//[Route("api/[controller]")]
//[ApiController]
//public class BooksController : ControllerBase
//{
//    private readonly BiblioApiDB _context;

//    public BooksController(BiblioApiDB context)
//    {
//        _context = context;
//    }




//        // GET: api/books <IEnumerable<Book>>
//        [HttpGet]
//        public async Task<ActionResult> GetBooks()
//        {

//            var books = await _context.Book.ToListAsync();
//            return Ok(books); // Возврат списка книг с кодом 200
//        }

//        // GET: api/books/{id}
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Book>> GetBook(int id)
//        {

//            var book = await _context.Book.FindAsync(id);
//            if (book == null)
//            {
//                return NotFound(new { Message = "Книга не найдена." });
//            }
//            return Ok(book); // Возврат найденной книги с кодом 200
//        }

//        // POST: api/books  <Book> [FromBody] 
//        [HttpPost]
//        public async Task<ActionResult> PostBook(Book book)
//        {

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }


//            var validationResults = new List<ValidationResult>();
//            var isValid = Validator.TryValidateObject(book, new ValidationContext(book), validationResults);
//            if (!isValid)
//            {
//                return BadRequest(validationResults.Select(r => r.ErrorMessage).ToArray());
//            }


//            await _context.Book.AddAsync(book);
//            await _context.SaveChangesAsync();

//            // Возврат созданной книги с кодом 201 и ссылкой на GET-запрос
//            return CreatedAtAction(nameof(GetBook), new { id = book.Id_Book }, book);
//        }

//        // PUT: api/books/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutBook(int id, [FromBody] Book book)
//        {


//            if (id != book.Id_Book)
//            {
//                return BadRequest(new { Message = "ID книги не совпадает." });
//            }

//            _context.Entry(book).State = EntityState.Modified;


//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!BookExists(id))
//                {
//                    return NotFound(new { Message = "Книга не найдена." });
//                }
//                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при обновлении книги.");
//            }

//            return NoContent(); // Возврат 204 No Content
//        }

//        // DELETE: api/books/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBook(int id)
//        {

//            var book = await _context.Book.FindAsync(id);
//            if (book == null)
//            {
//                return NotFound(new { Message = "Книга не найдена." });
//            }

//            _context.Book.Remove(book);
//            await _context.SaveChangesAsync();

//            return NoContent(); // Возврат 204 No Content
//        }

//        private bool BookExists(int id)
//        {
//            return _context.Book.Any(e => e.Id_Book == id);
//        }

//        [HttpGet("{id}/availability")]
//        public async Task<ActionResult<int>> GetAvailableCopies(int id)
//        {
//            var book = await _context.Book.FindAsync(id);
//            if (book == null) return NotFound();
//            return book.AvailableCopies;
//        }
//    }







//[HttpGet("books")]
//public IActionResult GetBooks([FromQuery] BookSearchFilter filter, [FromQuery] PaginationParams pagination)
//{
//    var query = _context.Book.AsQueryable();

//    if (!string.IsNullOrEmpty(filter.Genre))
//        query = query.Where(b => Convert.ToString(b.GenreID).Contains(filter.Genre)); // Assuming Genre navigation property with Name


//    var totalItems = query.Count();
//    var bookTitles = query
//        .Skip((pagination.Page - 1) * pagination.PageSize)
//        .Take(pagination.PageSize)
//        .Select(b => b.Title) // Select only the Title property
//        .ToList();

//    var response = new PagedResponse<List<string>>(bookTitles, pagination.Page, pagination.PageSize, totalItems);
//    return Ok(response);
//}

//public class BookSearchFilter
//{

//    public string Genre { get; set; }//Nullable int to handle optional year
//}

//public class PaginationParams
//{
//    public int Page { get; set; } = 1; //Default to page 1
//    public int PageSize { get; set; } = 10; //Default to 10 items per page

//    // Modified PagedResponse to handle List<string>
//    public class PagedResponse<T>
//    {
//        public PagedResponse(T data, int page, int pageSize, int totalItems)
//        {
//            Data = data;
//            Page = page;
//            PageSize = pageSize;
//            TotalItems = totalItems;
//        }

//        public T Data { get; set; }
//        public int Page { get; set; }
//        public int PageSize { get; set; }
//        public int TotalItems { get; set; }
//    }

//}


//[HttpGet("books")]
//public IActionResult GetBooks([FromQuery] BookSearchFilter filter, [FromQuery] PaginationParams pagination)
//{
//    //1. Get the queryable set of books from your data source (e.g., database context)
//    var query = _context.Book.AsQueryable(); //Replace _context with your DbContext

//    //2. Apply search and filter criteria
//    if (!string.IsNullOrEmpty(filter.Author))
//        query = query.Where(b => b.Author.Contains(filter.Author));
//    if (!string.IsNullOrEmpty(filter.Genre))
//        query = query.Where(b => Convert.ToString(b.GenreID).Contains(filter.Genre));
//    if (!string.IsNullOrEmpty(filter.Title))
//        query = query.Where(b => b.Title.Contains(filter.Title));


//    //3. Apply pagination
//    var totalItems = query.Count();
//    var books = query.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();


//    //4. Return the results with pagination metadata
//    var response = new PagedResponse<List<Book>>(books, pagination.Page, pagination.PageSize, totalItems);
//    return Ok(response);
//}

//public class BookSearchFilter
//{
//    public string Title { get; set; }
//    public string Author { get; set; }
//    public string Genre { get; set; }//Nullable int to handle optional year
//}

//public class PaginationParams
//{
//    public int Page { get; set; } = 1; //Default to page 1
//    public int PageSize { get; set; } = 10; //Default to 10 items per page
//}

//public class PagedResponse<T>
//{
//    public PagedResponse(List<Book> books, int page, int pageSize, int totalItems)
//    {
//        Books = books;
//        Page = page;
//        PageSize = pageSize;
//        TotalItems = totalItems;
//    }

//    public T Data { get; set; }
//    public int Page { get; set; }
//    public int PageSize { get; set; }
//    public int TotalItems { get; set; }
//    public List<Book> Books { get; }
//}
