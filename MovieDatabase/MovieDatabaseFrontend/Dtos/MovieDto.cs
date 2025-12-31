namespace MovieDatabaseBackend.Dtos
{
    public class MovieDto(int id, string title)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
    }
}
