using Biblioteka.DatabContext;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly BiblioApiDB _context;

    public GenresController(BiblioApiDB context)
    {
        _context = context;
    }

    // GET: api/genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        var genres = await _context.Genre.ToListAsync();
        if (genres == null || genres.Count == 0)
        {
            return NotFound(new { Message = "Жанры не найдены." });
        }
        return Ok(genres);
    }

    // GET: api/genres/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var genre = await _context.Genre.FindAsync(id);
        if (genre == null)
        {
            return NotFound(new { Message = "Жанр не найден." });
        }
        return Ok(genre);
    }

    // POST: api/genres
    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre([FromBody] Genre genre)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Genre.Add(genre);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id_Genre }, genre);
    }

    // PUT: api/genres/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, [FromBody] Genre genre)
    {
        if (id != genre.Id_Genre)
        {
            return BadRequest(new { Message = "ID жанра не совпадает." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(genre).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenreExists(id))
            {
                return NotFound(new { Message = "Жанр не найден." });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при обновлении жанра.");
        }

        return NoContent(); // Возврат 204 No Content
    }

    // DELETE: api/genres/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genre.FindAsync(id);
        if (genre == null)
        {
            return NotFound(new { Message = "Жанр не найден." });
        }

        _context.Genre.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent(); // Возврат 204 No Content
    }

    private bool GenreExists(int id)
    {
        return _context.Genre.Any(e => e.Id_Genre == id);
    }
}

