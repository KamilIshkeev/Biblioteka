using Biblioteka.DatabContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteka.Services
{
    public class RentalService : IRentalService
    {
        private readonly BiblioApiDB _context;

        public RentalService(BiblioApiDB context)
        {
            _context = context;
        }

        public async Task<ActionResult<Rental>> PostRentalAsync(Rental rental)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(rental, new ValidationContext(rental), validationResults, true))
            {
                return new BadRequestObjectResult(validationResults.Select(r => r.ErrorMessage));
            }

            try
            {
                await _context.Rental.AddAsync(rental);
                await _context.SaveChangesAsync();
                return new OkObjectResult(rental);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception with details for debugging (e.g., using Serilog)
                // Log.Error(ex, "Error saving book to database: {ExceptionMessage}", ex.Message); 
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            
        }


        public async Task<IActionResult> ReturnRentalAsync(int id)
        {
            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return new NotFoundObjectResult(new { Message = "Аренда не найдена." });
            }

            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByReaderAsync(int id)
        {
            var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
            return rentals == null || !rentals.Any() ? new NotFoundObjectResult(new { Message = "Аренд для этого читателя не найдено." }) : new OkObjectResult(rentals);
        }

        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByBookAsync(int id)
        {
            var rentals = await _context.Rental.Where(r => r.BookId == id).ToListAsync();
            return rentals == null || !rentals.Any() ? new NotFoundObjectResult(new { Message = "Аренд для этой книги не найдено." }) : new OkObjectResult(rentals);
        }

        public async Task<ActionResult<IEnumerable<Rental>>> GetCurrentRentalsAsync()
        {
            var currentRentals = await _context.Rental.Where(r => r.ReturnDate == null).ToListAsync();
            return currentRentals == null || !currentRentals.Any() ? new NotFoundObjectResult(new { Message = "Текущих аренд не найдено." }) : new OkObjectResult(currentRentals);
        }

        public async Task<ActionResult<Rental>> GetRentalAsync(int id)
        {
            var rental = await _context.Rental.FindAsync(id);
            return rental == null ? new NotFoundObjectResult(new { Message = "Аренда не найдена." }) : new OkObjectResult(rental);
        }
    }
}