using Biblioteka.DatabContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Biblioteka.Requests;

//using static Biblioteka.Services.ReaderService.PaginationParams;

namespace Biblioteka.Services
{
    public class ReaderService(BiblioApiDB context, IHttpContextAccessor httpContextAccessor  /*, Check check*/)/* : IReaderService*/ : IReaderService
    {
        private readonly BiblioApiDB _context = context;
        


        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly HttpClient _httpClient;


        public List<Reader> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var users = _context.Reader;
            var totalUsers = users.Count();
            if (page.HasValue && pageSize.HasValue)
            {
                var usersPaginated = users.Skip((int)((page - 1) * (int)pageSize)).Take((int)pageSize).ToList();
                return usersPaginated;
            }
            return users.ToList();

        }

        public async Task AddNewReader([FromQuery] createReader reader)
        {
            var check = await _context.Reader.FirstOrDefaultAsync(r => r.Login == reader.Login);
            var Reader = new Reader
            {
                Name = reader.Name,
                Password = reader.Password,
                Date_Birth = reader.Date_Birth,
                Login = reader.Login,
                Id_Role = 2
            };
            await _context.Reader.AddAsync(Reader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReaderById(int id)
        {
            var check = await _context.Reader.FirstOrDefaultAsync(r => r.Id_Reader == id);
            _context.Reader.Remove(check);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateReaderById(int id, [FromQuery] createReader reader)
        {
            var check = await _context.Reader.FirstOrDefaultAsync(r => r.Id_Reader == id);
            check.Name = reader.Name;
            check.Password = reader.Password;
            check.Date_Birth = reader.Date_Birth;
            check.Login = reader.Login;
            await _context.SaveChangesAsync();
        }

        public Reader GetReaderById(int id)
        {
            return _context.Reader.FirstOrDefault(r => r.Id_Reader == id);
        }

        public List<Rental> GetReadersRentals(int id)
        {
            return _context.Rental.Where(r => r.Id_Reader == id).Include(r => r.Reader).Include(r => r.Book).ToList();
        }

        public bool ReaderExists(string login)
        {
            return _context.Reader.Any(r => r.Login == login);
        }
        public List<Reader> GetAll()
        {
            return _context.Reader.ToList();
        }

       



        //public async Task<IEnumerable<Reader>> GetReadersAsync()
        //{
        //    var response = await _httpClient.GetAsync("");
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadFromJsonAsync<IEnumerable<Reader>>()!;
        //}

        //// Add other methods like GetReaderAsync, PostReaderAsync, etc., mirroring the ReaderServ API.
        //// Example for GetReaderAsync:
        //public async Task<Reader?> GetReaderAsync(int id)
        //{
        //    var response = await _httpClient.GetAsync($"{id}");
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadFromJsonAsync<Reader>();
        //}

        ////public async Task<IActionResult> GetReaders1Async([FromQuery] ReaderSearchFilter filter, [FromQuery] PaginationParams pagination)
        ////{

        ////var query = _context.Reader.AsQueryable();


        ////    if (filter.DateOfBirth.HasValue)
        ////        query = query.Where(b => b.DateOfBirth == filter.DateOfBirth.Value);

        ////    var totalItems = query.Count();
        ////    var lastName = query
        ////        .Skip((pagination.Page - 1) * pagination.PageSize)
        ////        .Take(pagination.PageSize)
        ////        .Select(b => b.LastName) 
        ////        .ToList();

        ////    var response = new PagedResponse<List<string>>(lastName, pagination.Page, pagination.PageSize, totalItems);
        ////    return new OkObjectResult(response);
        ////}

        ////public class ReaderSearchFilter
        ////{
        ////    public DateOnly? DateOfBirth { get; set; }

        ////}

        ////public class PaginationParams
        ////{
        ////    public int Page { get; set; } = 1; 
        ////    public int PageSize { get; set; } = 10; 


        ////    public class PagedResponse<T>
        ////    {
        ////        public PagedResponse(T data, int page, int pageSize, int totalItems)
        ////        {
        ////            page = 1;
        ////            Data = data;
        ////            Page = page;
        ////            PageSize = pageSize;
        ////            TotalItems = totalItems;
        ////        }

        ////        public T Data { get; set; }
        ////        public int Page { get; set; }
        ////        public int PageSize { get; set; }
        ////        public int TotalItems { get; set; }
        ////    }

        ////}


        ////public async Task<ActionResult<IEnumerable<Reader>>> GetReadersAsync()
        ////{
        ////    var readers = await _context.Reader.ToListAsync();
        ////    return readers == null || !readers.Any() ? new NotFoundObjectResult(new { Message = "Читатели не найдены." }) : new OkObjectResult(readers);
        ////}

        ////public async Task<ActionResult<Reader>> GetReaderAsync(int id)
        ////{
        ////    var reader = await _context.Reader.FindAsync(id);
        ////    return reader == null ? new NotFoundObjectResult(new { Message = "Читатель не найден." }) : new OkObjectResult(reader);
        ////}

        //public async Task<ActionResult<Reader>> PostReaderAsync(Reader reader)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("readers", reader);
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadFromJsonAsync<Reader>()!;

        //    //var validationResults = new List<ValidationResult>();
        //    //if (!Validator.TryValidateObject(reader, new ValidationContext(reader), validationResults, true))
        //    //{
        //    //    return new BadRequestObjectResult(validationResults.Select(r => r.ErrorMessage));
        //    //}

        //    //try
        //    //{
        //    //    await _context.Reader.AddAsync(reader);
        //    //    await _context.SaveChangesAsync();
        //    //    return new OkObjectResult(reader);
        //    //}
        //    //catch (DbUpdateException ex)
        //    //{

        //    //    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    //}
        //}

        //public async Task<IActionResult> PutReaderAsync(int id, Reader reader)
        //{


        //    var response = await _httpClient.PutAsJsonAsync($"readers/{id}", reader);
        //    response.EnsureSuccessStatusCode();
        //    return (IActionResult)await response.Content.ReadFromJsonAsync<Reader>()!;

        //    //if (id != reader.Id_Reader)
        //    //{
        //    //    return new BadRequestObjectResult(new { Message = "ID читателя не совпадает." });
        //    //}

        //    //_context.Entry(reader).State = EntityState.Modified;
        //    //try
        //    //{
        //    //    await _context.SaveChangesAsync();
        //    //    return new NoContentResult();
        //    //}
        //    //catch (DbUpdateConcurrencyException)
        //    //{
        //    //    if (!await ReaderExistsAsync(id))
        //    //    {
        //    //        return new NotFoundObjectResult(new { Message = "Читатель не найден." });
        //    //    }

        //    //    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    //}


        //}

        //public async Task<IActionResult> DeleteReaderAsync(int id)
        //{

        //    var response = await _httpClient.DeleteAsync($"readers/{id}");
        //    response.EnsureSuccessStatusCode();
        //    return (IActionResult)await response.Content.ReadFromJsonAsync<Reader>()!;

        //    //var reader = await _context.Reader.FindAsync(id);
        //    //if (reader == null)
        //    //{
        //    //    return new NotFoundObjectResult(new { Message = "Читатель не найден." });
        //    //}

        //    //_context.Reader.Remove(reader);
        //    //await _context.SaveChangesAsync();
        //    //return new NoContentResult();
        //}

        ////public async Task<ActionResult<IEnumerable<Rental>>> GetReaderRentalsAsync(int id)
        ////{
        ////    var rentals = await _context.Rental.Where(r => r.ReaderId == id).ToListAsync();
        ////    return rentals == null || !rentals.Any() ? new NotFoundObjectResult(new { Message = "Нет аренд для этого читателя." }) : new OkObjectResult(rentals);
        ////}

        //public async Task<bool> ReaderExistsAsync(int id)
        //{
        //    return await _context.Reader.AnyAsync(e => e.Id_Reader == id);
        //}


    }
}


