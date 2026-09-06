using System.Text.Json.Serialization;
namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = default!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookGenre Genre { get; set; } = BookGenre.Undefined;
        public int PublishedYear { get; set; }

        // Чутливі дані, які мають бути приховані від читачів
        public decimal PurchasePrice { get; set; }
        public string InternalNotes { get; set; } = string.Empty;
    }

    public enum BookGenre
    {
        Undefined,
        Fiction,
        Classics,
        Dystopia,
        Poetry,
        Fantasy,
        Horror
    }
}
