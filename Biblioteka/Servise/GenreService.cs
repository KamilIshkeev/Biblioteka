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



