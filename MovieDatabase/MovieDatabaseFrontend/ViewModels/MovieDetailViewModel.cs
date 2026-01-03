using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseFrontend.ViewModels
{
    public class MovieDetailViewModel
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Plot { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Rating { get; set; }
        public int? AgeRating { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; } = [];
        public IEnumerable<PersonViewModel> Directors { get; set; } = [];
        public PersonViewModel? Writer { get; set; }
        public IEnumerable<PersonViewModel> Actors { get; set; } = [];
        public int? Duration { get; set; }
    }
}
