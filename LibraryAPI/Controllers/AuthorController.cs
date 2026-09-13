using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Services;
using LibraryAPI.Requests;
using LibraryAPI.Models;
using LibraryAPI.DTOs;


namespace LibraryAPI.Controllers
{
    /*
     Я знаю що цього не було в завданні.
     Додати цей контроллер вже було моїм власним бажанням.
     */
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;
        public AuthorController(AuthorRepository authorRepository)      
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor([FromBody] CreateAuthorRequest request)
        {
            var createdAuthor = await _authorRepository.AddAuthor(request);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> UpdateAuthor(int id, [FromBody] CreateAuthorRequest request)
        {
            var updatedAuthor = await _authorRepository.UpdateAuthor(id, request);
            if (updatedAuthor == null)
            {
                return NotFound();
            }
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorRepository.DeleteAuthor(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
