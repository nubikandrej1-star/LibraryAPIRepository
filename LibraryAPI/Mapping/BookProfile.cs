// Mapping/BookProfile.cs
using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Requests;

namespace LibraryApi.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        // 1. Напрямок: Book -> BookDto (для виведення даних клієнту)
        CreateMap<Book, BookDTO>()
            .ForMember(dto => dto.FullNameAuthor, opt => opt.MapFrom(book => book.Author.GetFullName()));

        // 2. Напрямок: CreateBookRequest -> Book (для створення/оновлення книги)
        CreateMap<CreateBookRequest, Book>()
            .ForMember(book => book.AuthorId, opt => opt.MapFrom(request => request.AuthorId));

        CreateMap<CreateAuthorRequest, Author>();
    }
}