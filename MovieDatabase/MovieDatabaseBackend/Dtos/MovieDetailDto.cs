namespace MovieDatabaseBackend.Dtos
{
    public class MovieDetailDto(int id, string title)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string? Plot { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Rating { get; set; }
        public int? AgeRating { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; } = [];
        public IEnumerable<PersonDto> Directors { get; set; } = [];
        public PersonDto? Writer { get; set; }
        public IEnumerable<PersonDto> Actors { get; set; } = [];
        public int? Duration { get; set; }
    }
}
