using Biblioteka.DatabContext;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly BiblioApiDB _context;

    public RentalsController(BiblioApiDB context)
    {
        _context = context;
    }

    // POST: api/rentals
    [HttpPost]
    public async Task<ActionResult<Rental>> PostRental([FromBody] Rental rental)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Rental.Add(rental);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRental), new { id = rental.Id_Rental }, rental);
    }

    // PUT: api/rentals/return
    [HttpPut("return/{id}")]
    public async Task<IActionResult> ReturnRental(int id)
    {
        var rental = await _context.Rental.FindAsync(id);
        if (rental == null)
        {
            return NotFound(new { Message = "Аренда не найдена." });
        }

        _context.Rental.Remove(rental);
        await _context.SaveChangesAsync();

        return NoContent(); // Возврат 204 No Content
    }

    // GET: api/rentals/history/reader/{id}
    [HttpGet("history/reader/{id}")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByReader(int id)
    {
        var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
        if (rentals == null || rentals.Count == 0)
        {
            return NotFound(new { Message = "Аренд для этого читателя не найдено." });
        }
        return Ok(rentals);
    }

    // GET: api/rentals/history/book/{id}
    [HttpGet("history/book/{id}")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByBook(int id)
    {
        var rentals = await _context.Rental.Where(r => r.BookId == id).ToListAsync();
        if (rentals == null || rentals.Count == 0)
        {
            return NotFound(new { Message = "Аренд для этой книги не найдено." });
        }
        return Ok(rentals);
    }

    // GET: api/rentals/current
    [HttpGet("current")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetCurrentRentals()
    {
        var currentRentals = await _context.Rental.Where(r => r.ReturnDate == null).ToListAsync();
        if (currentRentals == null || currentRentals.Count == 0)
        {
            return NotFound(new { Message = "Текущих аренд не найдено." });
        }
        return Ok(currentRentals);
    }

    // GET: api/rentals/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Rental>> GetRental(int id)
    {
        var rental = await _context.Rental.FindAsync(id);
        if (rental == null)
        {
            return NotFound(new { Message = "Аренда не найдена." });
        }
        return Ok(rental);
    }
}




