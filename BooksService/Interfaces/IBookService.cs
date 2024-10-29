using BooksService.Model;
using Microsoft.AspNetCore.Mvc;
using static BooksService.Services.BookService;

namespace BooksService.Interfaces
{
    public interface IBookService
    {
        Task<IActionResult> GetBooks1Async([FromQuery] BookSearchFilter filter, [FromQuery] PaginationParams pagination);
        Task<IActionResult> GetBooksAsync();
        Task<IActionResult> GetBookAsync(int id);
        Task<IActionResult> PostBookAsync(Book book);
        Task<IActionResult> PutBookAsync(int id, Book book);
        Task<IActionResult> DeleteBookAsync(int id);
        Task<IActionResult> GetAvailableCopiesAsync(int id);
    }
}
