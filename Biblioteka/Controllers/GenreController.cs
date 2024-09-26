using Biblioteka.DatabContext;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        return await _context.Genre.ToListAsync();
    }

    // POST: api/genres
    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre(Genre genre)
    {
        _context.Genre.Add(genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGenre", new { id = genre.Id_Genre }, genre);
    }

    // PUT: api/genres/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, Genre genre)
    {
        if (id != genre.Id_Genre)
        {
            return BadRequest();
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
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/genres/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genre.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        _context.Genre.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenreExists(int id)
    {
        return _context.Genre.Any(e => e.Id_Genre == id);
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

//    [Route("api/[controller]")]
//    [ApiController]
//    public class GenresController : ControllerBase
//    {
//        private readonly BiblioApiDB _context;

//        public GenresController(BiblioApiDB context)
//        {
//            _context = context;
//        }

//        // GET: api/genres
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
//        {
//            return await _context.Genre.ToListAsync();
//        }

//        // POST: api/genres
//        [HttpPost]
//        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
//        {
//            _context.Genre.Add(genre);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(Genre), new { Name = genre.Name }, genre);
//        }

//        // PUT: api/genres/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutGenre(int id, Genre genre)
//        {
//            if (id != genre.Id_Genre)
//            {
//                return BadRequest();
//            }

//            _context.Entry(genre).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // DELETE: api/genres/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteGenre(int id)
//        {
//            var genre = await _context.Genre.FindAsync(id);
//            if (genre == null)
//            {
//                return NotFound();
//            }

//            _context.Genre.Remove(genre);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }



//}

