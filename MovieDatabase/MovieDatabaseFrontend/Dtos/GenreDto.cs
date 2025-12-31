namespace MovieDatabaseBackend.Dtos
{
    public class GenreDto(int id, string name)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}
