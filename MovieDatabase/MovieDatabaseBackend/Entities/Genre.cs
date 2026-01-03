namespace MovieDatabaseBackend.Entities
{
    public class Genre
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Movie> Movies { get; set; } = [];
    }
}
