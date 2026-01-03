using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseBackend.Dtos
{
    public class PersonCreateUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
    }
}
