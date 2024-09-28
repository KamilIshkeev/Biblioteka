using Biblioteka.DatabContext;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ReadersController : ControllerBase
{
    private readonly BiblioApiDB _context;

    public ReadersController(BiblioApiDB context)
    {
        _context = context;
    }

    // GET: api/readers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
    {
        var readers = await _context.Reader.ToListAsync();
        if (readers == null || !readers.Any())
        {
            return NotFound(new { Message = "Читатели не найдены." });
        }
        return Ok(readers);
    }

    // GET: api/readers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Reader>> GetReader(int id)
    {
        var reader = await _context.Reader.FindAsync(id);
        if (reader == null)
        {
            return NotFound(new { Message = "Читатель не найден." });
        }
        return Ok(reader);
    }

    // POST: api/readers
    [HttpPost]
    public async Task<ActionResult<Reader>> PostReader([FromBody] Reader reader)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Message = "Некорректные данные.", Errors = ModelState });
        }

        await _context.Reader.AddAsync(reader);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReader), new { id = reader.Id_Reader }, reader);
    }

    // PUT: api/readers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReader(int id, [FromBody] Reader reader)
    {
        if (id != reader.Id_Reader)
        {
            return BadRequest(new { Message = "ID читателя не совпадает." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new { Message = "Некорректные данные.", Errors = ModelState });
        }

        _context.Entry(reader).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReaderExists(id))
            {
                return NotFound(new { Message = "Читатель не найден." });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при обновлении читателя.");
        }

        return NoContent(); // Возврат 204 No Content
    }

    // DELETE: api/readers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader(int id)
    {
        var reader = await _context.Reader.FindAsync(id);
        if (reader == null)
        {
            return NotFound(new { Message = "Читатель не найден." });
        }

        _context.Reader.Remove(reader);
        await _context.SaveChangesAsync();

        return NoContent(); // Возврат 204 No Content
    }

    // GET: api/readers/{id}/rentals
    [HttpGet("{id}/rentals")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetReaderRentals(int id)
    {
        var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
        if (rentals == null || !rentals.Any())
        {
            return NotFound(new { Message = "Нет аренд для этого читателя." });
        }
        return Ok(rentals);
    }

    private bool ReaderExists(int id)
    {
        return _context.Reader.Any(e => e.Id_Reader == id);
    }
}


