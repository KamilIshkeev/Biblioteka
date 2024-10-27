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

    [HttpPost("RentBookById/{bookId}")]
    public async Task<IActionResult> RentBookById(int bookId, int readerId, int rentalTime)
    {
        return null;
    }
    [HttpGet("getReadersRentals/{id}")]
    public async Task<IActionResult> GetReadersRentals(int id)
    {
        return null;
    }
    [HttpPost("returnRent{rentId}")]
    public async Task<IActionResult> ReturnRent(int rentId)
    {
        return null;
    }
    [HttpGet("getCurrentRentals")]
    public async Task<IActionResult> GetCurrentRentals()
    {
        return null;
    }
    [HttpGet("getBookRentals/{id}")]
    public async Task<IActionResult> GetBookRentals(int id)
    {
        return null;
    }
}


