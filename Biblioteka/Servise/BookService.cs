using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteka.DatabContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Biblioteka.Services.BookService.PaginationParams;

namespace Biblioteka.Services
{
    public class BookService : IBookService
    {
        private readonly BiblioApiDB _context;

        public BookService(BiblioApiDB context)
        {
            _context = context;
        }


      
        public async Task<IActionResult> GetBooks1Async([FromQuery] BookSearchFilter filter, [FromQuery] PaginationParams pagination)
        {
            var query = _context.Book.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Genre))
                query = query.Where(b => Convert.ToString(b.GenreID).Contains(filter.Genre)); // Assuming Genre navigation property with Name


            var totalItems = query.Count();
            var bookTitles = query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(b => b.Title) // Select only the Title property
                .ToList();

            var response = new PagedResponse<List<string>>(bookTitles, pagination.Page, pagination.PageSize, totalItems);
            return new OkObjectResult(response);
        }

        public class BookSearchFilter
        {

            public string Genre { get; set; }//Nullable int to handle optional year
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

        public async Task<IActionResult> GetBooksAsync()
        {
            var books = await _context.Book.ToListAsync();
            return new OkObjectResult(books);
        }

        public async Task<IActionResult> GetBookAsync(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return new NotFoundObjectResult(new { Message = "Книга не найдена." });
            }
            return new OkObjectResult(book);
        }

        public async Task<IActionResult> PostBookAsync(Book book)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(book, new ValidationContext(book), validationResults, true))
            {
                return new BadRequestObjectResult(validationResults.Select(r => r.ErrorMessage));
            }

            try
            {
                await _context.Book.AddAsync(book);
                await _context.SaveChangesAsync();
                return new OkObjectResult(book);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception with details for debugging (e.g., using Serilog)
                // Log.Error(ex, "Error saving book to database: {ExceptionMessage}", ex.Message); 
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> PutBookAsync(int id, Book book)
        {
            if (id != book.Id_Book)
            {
                return new BadRequestObjectResult(new { Message = "ID книги не совпадает." });
            }

            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return new NotFoundObjectResult(new { Message = "Книга не найдена." });
                }
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new NoContentResult();
        }

        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return new NotFoundObjectResult(new { Message = "Книга не найдена." });
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<IActionResult> GetAvailableCopiesAsync(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null) return new NotFoundResult();
            return new OkObjectResult(book.AvailableCopies);
        }


        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id_Book == id);
        }
    }
}









//using Biblioteka.DatabContext;
//using Biblioteka.Intefaces;
//using Biblioteka.Model;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Core;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations;

//namespace Biblioteka.Servise
//{
//    public class BookService : IBookService
//    {

//        private readonly BiblioApiDB _context;

//        public BookService(BiblioApiDB context)
//        {
//            _context = context;
//        }

//        public async Task<ActionResult> GetBooks()
//        {

//            var books = await _context.Book.ToListAsync();
//            return new OkObjectResult(books); // Возврат списка книг с кодом 200
//        }

//        public async Task<ActionResult> PostBook(Book book)
//        {
//            await _context.Book.AddAsync(book);
//            await _context.SaveChangesAsync();

//            // Возврат созданной книги с кодом 201 и ссылкой на GET-запрос
//            return new CreatedAtActionResult(nameof(GetBooks), new { id = book.Id_Book }, book);
//        }
//    }
//}
