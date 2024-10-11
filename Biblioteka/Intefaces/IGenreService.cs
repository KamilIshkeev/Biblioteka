using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteka.Interfaces
{
    public interface IGenreService
    {
        Task<ActionResult<IEnumerable<Genre>>> GetGenresAsync();
        Task<ActionResult<Genre>> GetGenreAsync(int id);
        Task<ActionResult<Genre>> PostGenreAsync(Genre genre);
        Task<IActionResult> PutGenreAsync(int id, Genre genre);
        Task<IActionResult> DeleteGenreAsync(int id);
        Task<bool> GenreExistsAsync(int id); // Added for consistency
    }
}
