using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // ... (rest of the controller methods use _genreService) ...

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres() => await _genreService.GetGenresAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id) => await _genreService.GetGenreAsync(id);
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre([FromBody] Genre genre) => await _genreService.PostGenreAsync(genre);
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, [FromBody] Genre genre) => await _genreService.PutGenreAsync(id, genre);
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id) => await _genreService.DeleteGenreAsync(id);

    }
}



