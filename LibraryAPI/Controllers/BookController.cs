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
        public ActionResult<List<BookDTO>> GetAllBooks(string? author=null)
        {
            List<BookDTO> books = _bookRepository.GetAllBooks(author);
            return books.Count > 0 ? Ok(books) : NotFound();
        }

        [HttpGet("{id:int}")]
        public ActionResult<BookDTO> GetByIdBooks(int id)
        {
            BookDTO? book = _bookRepository.GetById(id);
            return book != null ? Ok(book) : NotFound();
        }

        [HttpPost]
        public ActionResult<BookDTO> CreateBook([FromBody] CreateBookRequest request)
        {
            BookDTO? createdBook = _bookRepository.AddBook(request);
            return CreatedAtAction(nameof(GetByIdBooks), new { id = createdBook.Id }, createdBook);
        }
        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteBook(int id)
        {
            if (_bookRepository.DeleteBook(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
