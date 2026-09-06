using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Requests
{
    public class CreateAuthorRequest
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;
        [Range(1, 2020, ErrorMessage = "Birth year must be a positive integer")]
        public int BirthYear { get; set; } = -1;
    }
}
