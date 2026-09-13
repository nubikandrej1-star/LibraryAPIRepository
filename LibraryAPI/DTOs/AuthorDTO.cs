namespace LibraryAPI.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int BirthYear { get; set; } = -1;
    }
}
