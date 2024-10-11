using Biblioteka.Interfaces;
using Biblioteka.Model;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;
using static Biblioteka.Services.ReaderService;


[Route("api/[controller]")]
[ApiController]
public class ReadersController : ControllerBase
{
    private readonly IReaderService _readerService;

    public ReadersController(IReaderService readerService)
    {
        _readerService = readerService;
    }

    [HttpGet("readers")]
    public async Task<IActionResult> GetReaders1([FromQuery] ReaderSearchFilter filter, [FromQuery] PaginationParams pagination) => await _readerService.GetReaders1Async(filter, pagination);


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reader>>> GetReaders() => await _readerService.GetReadersAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Reader>> GetReader(int id) => await _readerService.GetReaderAsync(id);

    [HttpPost]
    public async Task<ActionResult<Reader>> PostReader([FromBody] Reader reader) => await _readerService.PostReaderAsync(reader);

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReader(int id, [FromBody] Reader reader) => await _readerService.PutReaderAsync(id, reader);

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader(int id) => await _readerService.DeleteReaderAsync(id);

    [HttpGet("{id}/rentals")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetReaderRentals(int id) => await _readerService.GetReaderRentalsAsync(id);
}


