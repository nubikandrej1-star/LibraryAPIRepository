namespace LibraryAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int BirthYear { get; set; } = -1;

        public ICollection<Book> Books { get; set; } = new List<Book>();

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
