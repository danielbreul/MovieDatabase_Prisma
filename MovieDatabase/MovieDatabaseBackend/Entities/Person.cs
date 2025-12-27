namespace MovieDatabaseBackend.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Movie> DirectedMovies { get; set; } = [];
        public ICollection<Movie> WrittenMovies { get; set; } = [];
        public ICollection<Movie> ActedInMovies { get; set; } = [];
    }
}
