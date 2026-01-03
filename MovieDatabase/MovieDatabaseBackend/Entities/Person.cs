namespace MovieDatabaseBackend.Entities
{
    public class Person
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Movie> DirectedMovies { get; set; } = [];
        public ICollection<Movie> WrittenMovies { get; set; } = [];
        public ICollection<Movie> ActedInMovies { get; set; } = [];
    }
}
