using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseBackend.Dtos
{
    public class GenreCreateUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
