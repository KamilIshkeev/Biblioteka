﻿using Microsoft.AspNetCore.Mvc;
using Biblioteka.Interfaces;

using Biblioteka.Model;
using static Biblioteka.Services.BookService;

namespace Biblioteka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks1([FromQuery] BookSearchFilter filter, [FromQuery] PaginationParams pagination) => await _bookService.GetBooks1Async(filter,  pagination);

        [HttpGet]
        public async Task<IActionResult> GetBooks() => await _bookService.GetBooksAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id) => await _bookService.GetBookAsync(id);

        [HttpPost]
        public async Task<IActionResult> PostBook(Book book) => await _bookService.PostBookAsync(book);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book) => await _bookService.PutBookAsync(id, book);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id) => await _bookService.DeleteBookAsync(id);

        [HttpGet("{id}/availability")]
        public async Task<IActionResult> GetAvailableCopies(int id) => await _bookService.GetAvailableCopiesAsync(id);
    }
}







