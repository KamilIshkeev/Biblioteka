using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteka.Interfaces
{
    public interface IRentalService
    {
        Task<ActionResult<Rental>> PostRentalAsync(Rental rental);
        Task<IActionResult> ReturnRentalAsync(int id);
        Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByReaderAsync(int id);
        Task<ActionResult<IEnumerable<Rental>>> GetRentalHistoryByBookAsync(int id);
        Task<ActionResult<IEnumerable<Rental>>> GetCurrentRentalsAsync();
        Task<ActionResult<Rental>> GetRentalAsync(int id);
    }
}