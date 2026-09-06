using LibraryAPI.Models;
using System.Text.Json.Serialization;

namespace LibraryAPI.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FullNameAuthor { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookGenre Genre { get; set; } = BookGenre.Undefined;
        public int PublishedYear { get; set; }
    }
}
