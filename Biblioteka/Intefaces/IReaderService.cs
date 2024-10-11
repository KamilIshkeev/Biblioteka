using Biblioteka.Model;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Biblioteka.Services.ReaderService;

namespace Biblioteka.Interfaces
{
    public interface IReaderService
    {
        Task<IActionResult> GetReaders1Async([FromQuery] ReaderSearchFilter filter, [FromQuery] PaginationParams pagination);
        Task<ActionResult<IEnumerable<Reader>>> GetReadersAsync();
        Task<ActionResult<Reader>> GetReaderAsync(int id);
        Task<ActionResult<Reader>> PostReaderAsync(Reader reader);
        Task<IActionResult> PutReaderAsync(int id, Reader reader);
        Task<IActionResult> DeleteReaderAsync(int id);
        Task<ActionResult<IEnumerable<Rental>>> GetReaderRentalsAsync(int id);
        Task<bool> ReaderExistsAsync(int id);
        
    }
}