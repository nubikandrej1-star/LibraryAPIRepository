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

        public List<BookDTO> GetAllBooks(string? authorFullName = null)
        {
            if(!string.IsNullOrEmpty(authorFullName))
            {
                List<Book> filteredBooks = _context.Books
                    .Include(b => b.Author).AsQueryable()
                    .Where(b => (b.Author.FirstName + " " + b.Author.LastName)
                        .ToLower()
                        .Contains(authorFullName.ToLower()))
                    .ToList();
                return _mapper.Map<List<BookDTO>>(filteredBooks);
            }
            return _mapper.Map<List<BookDTO>>(_context.Books.Include(b => b.Author));
        }
        public BookDTO? GetById(int id)
        {
            Book? book = _context.Books.Include(b => b.Author).AsQueryable().FirstOrDefault(b => b.Id == id);
            return book != null ? _mapper.Map<BookDTO>(book) : null;
        }

        public BookDTO AddBook(CreateBookRequest book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            _context.Books.Add(bookEntity);
            _context.SaveChanges();
            return _mapper.Map<BookDTO>(bookEntity);
        }
        public bool DeleteBook(int id) // тип bool повірен для перевірки чи видалення пройшло успішно
        {
            Book? book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;// повертаємо true, якщо видалення пройшло успішно
            }
            return false;// повертаємо false, якщо книга не знайдена
        }
    }
}