using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseBackend.Dtos
{
    public class MovieCreateUpdateDto
    {
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }
        public string? Plot { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Rating { get; set; }
        public int? AgeRating { get; set; }
        public IEnumerable<int> Genres { get; set; } = [];
        public IEnumerable<int> Directors { get; set; } = [];
        public int? Writer { get; set; }
        public IEnumerable<int> Actors { get; set; } = [];
        public int? Duration { get; set; }
    }
}
