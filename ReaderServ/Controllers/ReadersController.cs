using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReaderServ.Interfaces;
using ReaderServ.Model;
using ReaderServ.Requests;
using ReaderServ.Services;
using System.ComponentModel.DataAnnotations;
using static ReaderServ.Services.ReaderService;

namespace ReaderServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReaderService _reader;
        //private readonly IRentService _rent;
        public ReadersController(IReaderService readerService/*, IRentService rent*/)
        {
            _reader = readerService;
            //_rent = rent;
        }

        [HttpGet("getAllReaders")]
        public async Task<ActionResult> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize)
        {

            return new OkObjectResult(new
            {
                readers = _reader.GetAllReaders(page, pageSize)
            });
        }

        [HttpPost("addNewReader")]
        public async Task<IActionResult> AddNewReader([FromQuery] createReader reader)
        {

            if (_reader.ReaderExists(reader.Login))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("reader with that login and password already exists")
                });
            }
            if (string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Password) || string.IsNullOrWhiteSpace(reader.Login) || string.IsNullOrWhiteSpace(reader.Date_Birth.ToString()))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            await _reader.AddNewReader(reader);
            return Ok();
        }

        [HttpPut("updateReaderById/{id}")]
        public async Task<IActionResult> UpdateReaderById(int id, [FromQuery] createReader reader)
        {

            if (!_reader.GetAll().Any(r => r.Id_Reader == id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }
            if (string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Password) || string.IsNullOrWhiteSpace(reader.Login) || string.IsNullOrWhiteSpace(reader.Date_Birth.ToString()))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            await _reader.UpdateReaderById(id, reader);
            return Ok();
        }

        [HttpDelete("deleteReaderById/{id}")]
        public async Task<IActionResult> DeleteReaderById(int id)
        {

            if (!_reader.GetAll().Any(r => r.Id_Reader == id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }
            await _reader.DeleteReaderById(id);
            return Ok();
        }

        [HttpGet("getReaderById{id}")]
        public async Task<IActionResult> GetReaderById(int id)
        {

            if (!_reader.GetAll().Any(r => r.Id_Reader == id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }

            return new OkObjectResult(new
            {
                reader = _reader.GetReaderById(id)
            });
        }
        //[HttpGet("getReadersBooks/{id}")]
        //public async Task<IActionResult> GetReadersRentals(int id)
        //{

        //    if (_rent.ReaderInRent(id))
        //    {
        //        return new NotFoundObjectResult(new
        //        {
        //            error = NotFound("reader has no rentals")
        //        });
        //    }
        //    return new OkObjectResult(new
        //    {
        //        books = _reader.GetReadersRentals(id)
        //    });
        //}
        [HttpGet("isAdmin")]
        public async Task<IActionResult> checkRole()
        {

            return Ok("you admin");
        }

    }
}