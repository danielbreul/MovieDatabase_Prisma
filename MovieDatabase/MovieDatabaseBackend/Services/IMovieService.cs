using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IMovieService
    {
        IEnumerable<MovieDto> GetMovies(string? title = null);
        MovieDetailDto? GetMovie(int id);
        void CreateMovie(MovieCreateUpdateDto movie);
        void UpdateMovie(int id, MovieCreateUpdateDto movie);
        void DeleteMovie(int id);
    }
}
