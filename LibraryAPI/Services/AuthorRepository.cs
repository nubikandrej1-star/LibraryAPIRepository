using AutoMapper;
using LibraryAPI.Models;
using LibraryAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Requests;

namespace LibraryAPI.Services
{
    public class AuthorRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public AuthorRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AuthorDTO>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors.Select(author => _mapper.Map<AuthorDTO>(author)).ToList();
        }

        public async Task<AuthorDTO?> GetById(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            return author != null ? _mapper.Map<AuthorDTO>(author) : null;
        }

        public async Task<AuthorDTO> AddAuthor(CreateAuthorRequest author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _context.Authors.Add(authorEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AuthorDTO>(authorEntity);
        }

        public async Task<AuthorDTO?> UpdateAuthor(int id, CreateAuthorRequest author)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
            {
                return null;
            }
            _mapper.Map(author, existingAuthor);
            await _context.SaveChangesAsync();
            return _mapper.Map<AuthorDTO>(existingAuthor);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return false;
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
