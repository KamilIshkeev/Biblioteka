using Biblioteka.DatabContext;
using Biblioteka.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]

public class ReadersController : ControllerBase
{
    private readonly BiblioApiDB _context;

    public ReadersController(BiblioApiDB context)
    {
        _context = context;
    }

    // GET: api/readers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
    {
        return await _context.Reader.ToListAsync();
    }

    // GET: api/readers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Reader>> GetReader(int id)
    {
        var reader = await _context.Reader.FindAsync(id);

        if (reader == null)
        {
            return NotFound();
        }

        return reader;
    }

    // POST: api/readers
    [HttpPost]
    public async Task<ActionResult<Reader>> PostReader(Reader reader)
    {
        _context.Reader.Add(reader);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetReader", new { id = reader.Id_Reader }, reader);
    }

    // PUT: api/readers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReader(int id, Reader reader)
    {
        if (id != reader.Id_Reader)
        {
            return BadRequest();
        }

        _context.Entry(reader).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReaderExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/readers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader(int id)
    {
        var reader = await _context.Reader.FindAsync(id);
        if (reader == null)
        {
            return NotFound();
        }

        _context.Reader.Remove(reader);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/readers/{id}/rentals
    [HttpGet("{id}/rentals")]
    public async Task<ActionResult<IEnumerable<Rental>>> GetReaderRentals(int id)
    {
        var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
        return rentals;
    }

    private bool ReaderExists(int id)
    {
        return _context.Reader.Any(e => e.Id_Reader == id);
    }
}















//using Microsoft.AspNetCore.Authorization; // Добавьте этот импорт
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Biblioteka.DatabContext;
//using Biblioteka.Model;

//[Route("api/[controller]")]
//[ApiController]
//[Authorize] // Этот атрибут требует аутентификации для всех методов контроллера
//public class ReadersController : ControllerBase
//{
//    private readonly BiblioApiDB _context;

//    public ReadersController(BiblioApiDB context)
//    {
//        _context = context;
//    }

//    // GET: api/readers
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
//    {
//        return await _context.Reader.ToListAsync();
//    }

//    // GET: api/readers/{id}
//    [HttpGet("{id}")]
//    public async Task<ActionResult<Reader>> GetReader(int id)
//    {
//        var reader = await _context.Reader.FindAsync(id);

//        if (reader == null)
//        {
//            return NotFound();
//        }

//        return reader;
//    }

//    // POST: api/readers
//    [HttpPost]
//    public async Task<ActionResult<Reader>> PostReader(Reader reader)
//    {
//        _context.Reader.Add(reader);
//        await _context.SaveChangesAsync();

//        return CreatedAtAction("GetReader", new { id = reader.Id_Reader }, reader);
//    }

//    // PUT: api/readers/{id}
//    [HttpPut("{id}")]
//    public async Task<IActionResult> PutReader(int id, Reader reader)
//    {
//        if (id != reader.Id_Reader)
//        {
//            return BadRequest();
//        }

//        _context.Entry(reader).State = EntityState.Modified;

//        try
//        {
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!ReaderExists(id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }

//        return NoContent();
//    }

//    // DELETE: api/readers/{id}
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteReader(int id)
//    {
//        var reader = await _context.Reader.FindAsync(id);
//        if (reader == null)
//        {
//            return NotFound();
//        }

//        _context.Reader.Remove(reader);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }

//    // GET: api/readers/{id}/rentals
//    [HttpGet("{id}/rentals")]
//    public async Task<ActionResult<IEnumerable<Rental>>> GetReaderRentals(int id)
//    {
//        var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
//        return rentals;
//    }

//    private bool ReaderExists(int id)
//    {
//        return _context.Reader.Any(e => e.Id_Reader == id);
//    }
//}





















//using Biblioteka.DatabContext;
//using Biblioteka.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Biblioteka.Controllers
//{

//        // ReadersController.cs
//        [Route("api/[controller]")]
//        [ApiController]
//        public class ReadersController : ControllerBase
//        {
//            private readonly BiblioApiDB _context;

//            public ReadersController(BiblioApiDB context)
//            {
//                _context = context;
//            }

//            // GET: api/readers
//            [HttpGet]
//            public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
//            {
//                return await _context.Reader.ToListAsync();
//            }

//            // GET: api/readers/5
//            [HttpGet("{id}")]
//            public async Task<ActionResult<Reader>> GetReader(int id)
//            {
//                var reader = await _context.Reader.FindAsync(id);

//                if (reader == null)
//                {
//                    return NotFound();
//                }

//                return reader;
//            }

//            // POST: api/readers
//            [HttpPost]
//            public async Task<ActionResult<Reader>> PostReader(Reader reader)
//            {
//                _context.Reader.Add(reader);
//                await _context.SaveChangesAsync();

//                return CreatedAtAction(nameof(GetReader), new { id = reader.Id_Reader }, reader);
//            }

//            // PUT: api/readers/5
//            [HttpPut("{id}")]
//            public async Task<IActionResult> PutReader(int id, Reader reader)
//            {
//            if (id != reader.Id_Reader)
//            {
//                return BadRequest();
//            }

//            _context.Entry(reader).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();




//        }

//            // DELETE: api/readers/5
//            [HttpDelete("{id}")]
//            public async Task<IActionResult> DeleteReader(int id)
//            {
//                var reader = await _context.Reader.FindAsync(id);
//                if (reader == null)
//                {
//                    return NotFound();
//                }

//                _context.Reader.Remove(reader);
//                await _context.SaveChangesAsync();

//                return NoContent();
//            }

//            // GET: api/readers/rentals/5
//            [HttpGet("rentals/{readerId}")]
//            public async Task<ActionResult<IEnumerable<Rental>>> GetRentalsByReader(int readerId)
//            {
//                return await _context.Rental
//                    .Where(r => r.ReaderId == readerId)
//                    .ToListAsync();
//            }
//        }





//    }





