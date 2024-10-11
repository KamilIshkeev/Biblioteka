using Biblioteka.DatabContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Biblioteka.Services.ReaderService.PaginationParams;

namespace Biblioteka.Services
{
    public class ReaderService : IReaderService
    {
        private readonly BiblioApiDB _context;

        public ReaderService(BiblioApiDB context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetReaders1Async([FromQuery] ReaderSearchFilter filter, [FromQuery] PaginationParams pagination)
        {
           
        var query = _context.Reader.AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(b => b.FirstName.Contains(filter.FirstName)); // Assuming Genre navigation property with Name


            var totalItems = query.Count();
            var lastName = query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(b => b.LastName) // Select only the Title property
                .ToList();

            var response = new PagedResponse<List<string>>(lastName, pagination.Page, pagination.PageSize, totalItems);
            return new OkObjectResult(response);
        }

        public class ReaderSearchFilter
        {

            public string FirstName { get; set; }//Nullable int to handle optional year
        }

        public class PaginationParams
        {
            public int Page { get; set; } = 1; //Default to page 1
            public int PageSize { get; set; } = 10; //Default to 10 items per page

            // Modified PagedResponse to handle List<string>
            public class PagedResponse<T>
            {
                public PagedResponse(T data, int page, int pageSize, int totalItems)
                {
                    page = 1;
                    Data = data;
                    Page = page;
                    PageSize = pageSize;
                    TotalItems = totalItems;
                }

                public T Data { get; set; }
                public int Page { get; set; }
                public int PageSize { get; set; }
                public int TotalItems { get; set; }
            }

        }


        public async Task<ActionResult<IEnumerable<Reader>>> GetReadersAsync()
        {
            var readers = await _context.Reader.ToListAsync();
            return readers == null || !readers.Any() ? new NotFoundObjectResult(new { Message = "Читатели не найдены." }) : new OkObjectResult(readers);
        }

        public async Task<ActionResult<Reader>> GetReaderAsync(int id)
        {
            var reader = await _context.Reader.FindAsync(id);
            return reader == null ? new NotFoundObjectResult(new { Message = "Читатель не найден." }) : new OkObjectResult(reader);
        }

        public async Task<ActionResult<Reader>> PostReaderAsync(Reader reader)
        {

            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(reader, new ValidationContext(reader), validationResults, true))
            {
                return new BadRequestObjectResult(validationResults.Select(r => r.ErrorMessage));
            }

            try
            {
                await _context.Reader.AddAsync(reader);
                await _context.SaveChangesAsync();
                return new OkObjectResult(reader);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception with details for debugging (e.g., using Serilog)
                // Log.Error(ex, "Error saving book to database: {ExceptionMessage}", ex.Message); 
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> PutReaderAsync(int id, Reader reader)
        {
            if (id != reader.Id_Reader)
            {
                return new BadRequestObjectResult(new { Message = "ID читателя не совпадает." });
            }

            _context.Entry(reader).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return new NoContentResult();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ReaderExistsAsync(id))
                {
                    return new NotFoundObjectResult(new { Message = "Читатель не найден." });
                }
                // Log the exception for debugging.  Example using Serilog:  Log.Error(ex, "Error updating genre");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            
        }

        public async Task<IActionResult> DeleteReaderAsync(int id)
        {
            var reader = await _context.Reader.FindAsync(id);
            if (reader == null)
            {
                return new NotFoundObjectResult(new { Message = "Читатель не найден." });
            }

            _context.Reader.Remove(reader);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult<IEnumerable<Rental>>> GetReaderRentalsAsync(int id)
        {
            var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
            return rentals == null || !rentals.Any() ? new NotFoundObjectResult(new { Message = "Нет аренд для этого читателя." }) : new OkObjectResult(rentals);
        }

        public async Task<bool> ReaderExistsAsync(int id)
        {
            return await _context.Reader.AnyAsync(e => e.Id_Reader == id);
        }

        
    }
}