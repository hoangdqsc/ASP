using Application_Temp.DTOs;
using Application_Temp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UJUTN_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            if (id != bookDto.Id)
                return BadRequest();

            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
