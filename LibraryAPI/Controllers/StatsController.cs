using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public StatsController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("books")]
        public async Task<ActionResult<object>> GetBookStats()
        {
            var count = await _bookRepository.GetCountBooks();
            var averageYear = await _bookRepository.GetAveragePublishedYear();

            return Ok(new
            {
                totalBooks = count,
                averagePublishedYear = averageYear
            });
        }
    }
}
