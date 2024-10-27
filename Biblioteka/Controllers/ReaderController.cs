using Biblioteka.Interfaces;
using Biblioteka.Model;
using Biblioteka.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Biblioteka.Services.ReaderService;
using Biblioteka.Requests;


[Route("api/[controller]")]
[ApiController]
public class ReadersController : ControllerBase
{
    private readonly IReaderService _readerService;

    public ReadersController(IReaderService readerService)
    {
        _readerService = readerService;
    }

    //[HttpGet("readers")]
    //public async Task<IActionResult> GetReaders1([FromQuery] ReaderSearchFilter filter, [FromQuery] PaginationParams pagination)/* => await _readerService.GetReaders1Async(filter, pagination);*/
    //{
    //    return null;
    //}

    [HttpGet("getAllReaders")]
    public async Task<ActionResult> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        return null;
    }
    [HttpPost("addNewReader")]
    public async Task<IActionResult> AddNewReader([FromQuery] createReader reader)
    {
        return null;
    }
    [HttpPut("updateReaderById/{id}")]
    public async Task<IActionResult> UpdateReaderById(int id, [FromQuery] createReader reader)
    {
        return null;
    }
    [HttpDelete("deleteReaderById/{id}")]
    public async Task<IActionResult> DeleteReaderById(int id)
    {
        return null;
    }
    [HttpGet("getReaderById{id}")]
    public async Task<IActionResult> GetReaderById(int id)
    {
        return null;
    }
    [HttpGet("getReadersBooks/{id}")]
    public async Task<IActionResult> GetReadersRentals(int id)
    {
        return null;
    }
    [HttpGet("isAdmin")]
    public async Task<IActionResult> checkRole()
    {
        return null;
    }

    //[HttpGet("{id}/rentals")]
    //public async Task<ActionResult<IEnumerable<Rental>>> GetReaderRentals(int id)/* => await _readerService.GetReaderRentalsAsync(id);*/
    //{
    //    return Ok();
    //}

}


