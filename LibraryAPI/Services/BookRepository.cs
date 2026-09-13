using AutoMapper;
using LibraryAPI.Models;
using LibraryAPI.DTOs;
using LibraryAPI.Requests;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public BookRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Get methods
        public async Task<List<BookDTO>> GetAllBooks(string? authorFullName = null)
        {
            if(!string.IsNullOrEmpty(authorFullName))
            {
                List<Book> filteredBooks = await _context.Books
                    .Include(b => b.Author).AsQueryable()
                    .Where(b => (b.Author.FirstName + " " + b.Author.LastName)
                        .ToLower()
                        .Contains(authorFullName.ToLower()))
                    .ToListAsync();
                return _mapper.Map<List<BookDTO>>(filteredBooks);
            }
            return _mapper.Map<List<BookDTO>>(await _context.Books.Include(b => b.Author).ToListAsync());
        }
        public async Task<BookDTO?> GetById(int id)
        {
            Book? book = await _context.Books.Include(b => b.Author).AsQueryable().FirstOrDefaultAsync(b => b.Id == id);
            return book != null ? _mapper.Map<BookDTO>(book) : null;
        }

        public async Task<List<BookDTO>?> GetByGenre(string genre)
        {
            List<Book> books = await _context.Books.Include(b => b.Author).AsQueryable().Where(b => b.Genre.ToString().ToLower() == genre.ToLower()).ToListAsync();
            return books.Count > 0 ? _mapper.Map<List<BookDTO>>(books) : null;
        }

        public async Task<int> GetCountBooks()
        {
            return await _context.Books.CountAsync();
        }

        public async Task<double> GetAveragePublishedYear()
        {
            return await _context.Books.AverageAsync(b => b.PublishedYear);
        }
        #endregion

        public async Task<BookDTO> AddBook(CreateBookRequest book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            _context.Books.Add(bookEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<BookDTO>(bookEntity);
        }
        public async Task<BookDTO?> UpdateBook(int id, CreateBookRequest book)
        {
            Book? existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (existingBook != null)
            {
                _mapper.Map(book, existingBook);
                await _context.SaveChangesAsync();
                return _mapper.Map<BookDTO>(existingBook);
            }
            return null;
        }
        public async Task<bool> DeleteBook(int id) // тип bool повірен для перевірки чи видалення пройшло успішно
        {
            Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;// повертаємо true, якщо видалення пройшло успішно
            }
            return false;// повертаємо false, якщо книга не знайдена
        }
    }
}