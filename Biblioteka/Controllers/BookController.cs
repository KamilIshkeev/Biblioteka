using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteka.DatabContext;
using Biblioteka.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        
        var books = await _context.Book.ToListAsync();
        return Ok(books); // Возврат списка книг с кодом 200
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        
        var book = await _context.Book.FindAsync(id);
        if (book == null)
        {
            return NotFound(new { Message = "Книга не найдена." });
        }
        return Ok(book); // Возврат найденной книги с кодом 200
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

   
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(book, new ValidationContext(book), validationResults);
        if (!isValid)
        {
            return BadRequest(validationResults.Select(r => r.ErrorMessage).ToArray());
        }

     
        await _context.Book.AddAsync(book);
        await _context.SaveChangesAsync();

        // Возврат созданной книги с кодом 201 и ссылкой на GET-запрос
        return CreatedAtAction(nameof(GetBook), new { id = book.Id_Book }, book);
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, [FromBody] Book book)
    {


        if (id != book.Id_Book)
        {
            return BadRequest(new { Message = "ID книги не совпадает." });
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
                return NotFound(new { Message = "Книга не найдена." });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при обновлении книги.");
        }

        return NoContent(); // Возврат 204 No Content
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
       
        var book = await _context.Book.FindAsync(id);
        if (book == null)
        {
            return NotFound(new { Message = "Книга не найдена." });
        }

        _context.Book.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent(); // Возврат 204 No Content
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.Id_Book == id);
    }

    [HttpGet("{id}/availability")]
    public async Task<ActionResult<int>> GetAvailableCopies(int id)
    {
        var book = await _context.Book.FindAsync(id);
        if (book == null) return NotFound();
        return book.AvailableCopies;
    }
}




