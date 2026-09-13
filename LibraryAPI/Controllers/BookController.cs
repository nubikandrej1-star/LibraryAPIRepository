using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Requests;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAllBooks(string? author=null)
        {
            List<BookDTO> books = await _bookRepository.GetAllBooks(author);
            return books.Count > 0 ? Ok(books) : NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDTO>> GetByIdBooks(int id)
        {
            BookDTO? book = await _bookRepository.GetById(id);
            return book != null ? Ok(book) : NotFound();
        }

        [HttpGet("by-genre")]
        public async Task<ActionResult<List<BookDTO>>> GetByGenre(string genre)
        {
            List<BookDTO>? books = await _bookRepository.GetByGenre(genre);
            return books != null ? Ok(books) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] CreateBookRequest request)
        {
            BookDTO? createdBook = await _bookRepository.AddBook(request);
            return CreatedAtAction(nameof(GetByIdBooks), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BookDTO>> UpdateBook(int id, [FromBody] CreateBookRequest request)
        {
            BookDTO? updatedBook = await _bookRepository.UpdateBook(id, request);
            return updatedBook != null ? Ok(updatedBook) : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (await _bookRepository.DeleteBook(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
