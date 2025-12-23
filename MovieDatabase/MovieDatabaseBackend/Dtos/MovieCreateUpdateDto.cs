using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseBackend.Dtos
{
    public class MovieCreateUpdateDto(string title)
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = title;
        public string? Plot { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Rating { get; set; }
        public int? AgeRating { get; set; }
        public IEnumerable<string> Genres { get; set; } = [];
        public IEnumerable<string> Directors { get; set; } = [];
        public string? Writer { get; set; }
        public IEnumerable<string> LeadActors { get; set; } = [];
        public int? Duration { get; set; }
    }
}
