namespace MovieDatabaseBackend.Entities
{
    public class Movie
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Plot { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Rating { get; set; }
        public int? AgeRating { get; set; }
        public ICollection<Genre> Genres { get; set; } = [];
        public ICollection<Person> Directors { get; set; } = [];
        public Person? Writer { get; set; }
        public ICollection<Person> Actors { get; set; } = [];
        public int? Duration { get; set; }
    }
}
