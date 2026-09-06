using LibraryAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryAPI.Requests
{
    public class CreateBookRequest
    {
        [Required(ErrorMessage = "Назва книги є обов'язковою.")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Автор є обов'язковим.")]
        [Range(1, int.MaxValue, ErrorMessage = "AuthorId must be a positive integer.")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Жанр є обов'язковим.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookGenre Genre { get; set; } = BookGenre.Undefined;

        [Range(1000, 2100)]
        public int PublishedYear { get; set; }
    }
}
