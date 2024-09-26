using Biblioteka.DatabContext;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly BiblioApiDB _context;

    public RentalsController(BiblioApiDB context)
    {
        _context = context;
    }

    // POST: api/rentals
    [HttpPost]
    public async Task<ActionResult<Rental>> PostRental(Rental rental)
    {
        _context.Rental.Add(rental);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRental", new { id = rental.Id_Rental }, rental);
    }

    // PUT: api/rentals/return
    [HttpPut("return")]
    public async Task<IActionResult> ReturnRental(int id)
    {
        var rental = await _context.Rental.FindAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        _context.Rental.Remove(rental);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/rentals/history/reader/{id}
    [HttpGet("history/reader/{id}")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByReader(int id)
    {
        var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
        return rentals;
    }

    // GET: api/rentals/history/book/{id}
    [HttpGet("history/book/{id}")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByBook(int id)
    {
        var rentals = await _context.Rental.Where(r => r.BookId == id).ToListAsync();
        return rentals;
    }

    // GET: api/rentals/current
    [HttpGet("current")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetCurrentRentals()
    {
        var currentRentals = await _context.Rental.Where(r => r.ReturnDate == null).ToListAsync();
        return currentRentals;
    }
}
































//using Biblioteka.DatabContext;
//using Biblioteka.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Http;
//using Biblioteka.Requests;

//namespace Biblioteka.Controllers
//{
//    // RentalsController.cs
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RentalsController : ControllerBase
//    {
//        private readonly BiblioApiDB _context;

//        public RentalsController(BiblioApiDB context)
//        {
//            _context = context;
//        }

//        // POST: api/rentals
//        [HttpPost]
//        public async Task<ActionResult<Rental>> PostRental(Rental rental)
//        {
//            _context.Rental.Add(rental);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(Rental), new { id = rental.Id_Rental }, rental);
//        }

//        // PUT: api/rentals/return/1
//        [HttpPut("return/{rentalId}")]
//        public async Task<IActionResult> ReturnRental(int rentalId)
//        {
//            var rental = await _context.Rental.FindAsync(rentalId);
//            if (rental == null)
//            {
//                return NotFound();
//            }

//            rental.Returned = true;
//            rental.ReturnDate = DateTime.UtcNow;
//            _context.Entry(rental).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // GET: api/rentals/history/1
//        [HttpGet("history/{readerId}")]
//        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByReader(int readerId)
//        {
//            return await _context.Rental
//                .Where(r => r.ReaderId == readerId)
//                .ToListAsync();
//        }

//        // GET: api/rentals/history/book/1
//        [HttpGet("history/book/{bookId}")]
//        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByBook(int bookId)
//        {
//            return await _context.Rental
//                .Where(r => r.BookId == bookId)
//                .ToListAsync();
//        }

//        // GET: api/rentals/current
//        [HttpGet("current")]
//        public async Task<ActionResult<IEnumerable<Rental>>> GetCurrentRentals()
//        {
//            return await _context.Rental
//                .Where(r => !r.Returned)
//                .ToListAsync();
//        }
//    }
//}
