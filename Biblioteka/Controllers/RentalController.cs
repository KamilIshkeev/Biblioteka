using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpPost]
    public async Task<ActionResult<Rental>> PostRental([FromBody] Rental rental) => await _rentalService.PostRentalAsync(rental);

    [HttpPut("return/{id}")]
    public async Task<IActionResult> ReturnRental(int id) => await _rentalService.ReturnRentalAsync(id);

    [HttpGet("history/reader/{id}")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByReader(int id) => await _rentalService.GetRentalHistoryByReaderAsync(id);

    [HttpGet("history/book/{id}")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByBook(int id) => await _rentalService.GetRentalHistoryByBookAsync(id);

    [HttpGet("current")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetCurrentRentals() => await _rentalService.GetCurrentRentalsAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Rental>> GetRental(int id) => await _rentalService.GetRentalAsync(id);
}


