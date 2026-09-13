using AutoMapper;
using LibraryAPI.Models;
using LibraryAPI.Requests;
using LibraryAPI.DTOs;

namespace LibraryAPI.Mapping
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            // 1. Напрямок: Author -> AuthorDto (для виведення даних клієнту)
            CreateMap<Author, AuthorDTO>()
                .ForMember(dto => dto.FullName, opt => opt.MapFrom(author => author.GetFullName()));
            // 2. Напрямок: CreateAuthorRequest -> Author (для створення/оновлення автора)
            CreateMap<CreateAuthorRequest, Author>();
        }
    }
}
