using Biblioteka.DatabContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Biblioteka.Services
{
    public class GenreService : IGenreService
    {
        private readonly BiblioApiDB _context;

        public GenreService(BiblioApiDB context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Genre>>> GetGenresAsync()
        {
            var genres = await _context.Genre.ToListAsync();
            return genres == null || genres.Count == 0 ? new NotFoundObjectResult(new { Message = "Жанры не найдены." }) : new OkObjectResult(genres);
        }

        public async Task<ActionResult<Genre>> GetGenreAsync(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            return genre == null ? new NotFoundObjectResult(new { Message = "Жанр не найден." }) : new OkObjectResult(genre);
        }

        public async Task<ActionResult<Genre>> PostGenreAsync(Genre genre)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(genre, new ValidationContext(genre), validationResults, true))
            {
                return new BadRequestObjectResult(validationResults.Select(r => r.ErrorMessage));
            }

            try
            {
                await _context.Genre.AddAsync(genre);
                await _context.SaveChangesAsync();
                return new OkObjectResult(genre);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception with details for debugging (e.g., using Serilog)
                // Log.Error(ex, "Error saving book to database: {ExceptionMessage}", ex.Message); 
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> PutGenreAsync(int id, Genre genre)
        {
            if (id != genre.Id_Genre)
            {
                return new BadRequestObjectResult(new { Message = "ID жанра не совпадает." });
            }

            _context.Entry(genre).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return new NoContentResult();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GenreExistsAsync(id))
                {
                    return new NotFoundObjectResult(new { Message = "Жанр не найден." });
                }
                // Log the exception for debugging.  Example using Serilog:  Log.Error(ex, "Error updating genre");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            if (genre == null)
            {
                return new NotFoundObjectResult(new { Message = "Жанр не найден." });
            }

            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<bool> GenreExistsAsync(int id)
        {
            return await _context.Genre.AnyAsync(e => e.Id_Genre == id);
        }
    }
}


















//using Biblioteka.DatabContext;
//using Biblioteka.Interfaces;
//using Biblioteka.Model;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Biblioteka.Services
//{
//    public class GenreService : IGenreService
//    {
//        private readonly BiblioApiDB _context;

//        public GenreService(BiblioApiDB context)
//        {
//            _context = context;
//        }

//        public async Task<ActionResult<IEnumerable<Genre>>> GetGenresAsync()
//        {
//            var genres = await _context.Genre.ToListAsync();
//            return genres == null || genres.Count == 0 ? NotFound(new { Message = "Жанры не найдены." }) : Ok(genres);
//        }

//        public async Task<ActionResult<Genre>> GetGenreAsync(int id)
//        {
//            var genre = await _context.Genre.FindAsync(id);
//            return genre == null ? NotFound(new { Message = "Жанр не найден." }) : Ok(genre);
//        }

//        public async Task<ActionResult<Genre>> PostGenreAsync(Genre genre)
//        {
//            _context.Genre.Add(genre);
//            await _context.SaveChangesAsync();
//            return CreatedAtAction(nameof(GetGenreAsync), new { id = genre.Id_Genre }, genre);
//        }

//        public async Task<IActionResult> PutGenreAsync(int id, Genre genre)
//        {
//            if (id != genre.Id_Genre)
//            {
//                return BadRequest(new { Message = "ID жанра не совпадает." });
//            }

//            _context.Entry(genre).State = EntityState.Modified;
//            try
//            {
//                await _context.SaveChangesAsync();
//                return NoContent();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!await GenreExistsAsync(id))
//                {
//                    return NotFound(new { Message = "Жанр не найден." });
//                }
//                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при обновлении жанра.");
//            }
//        }

//        public async Task<IActionResult> DeleteGenreAsync(int id)
//        {
//            var genre = await _context.Genre.FindAsync(id);
//            if (genre == null)
//            {
//                return NotFound(new { Message = "Жанр не найден." });
//            }

//            _context.Genre.Remove(genre);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }

//        public async Task<bool> GenreExistsAsync(int id)
//        {
//            return await _context.Genre.AnyAsync(e => e.Id_Genre == id);
//        }
//    }
//}
