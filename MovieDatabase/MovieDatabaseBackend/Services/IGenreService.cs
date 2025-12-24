using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IGenreService
    {
        public IEnumerable<MovieDto> GetMovies(int genreId);
    }
}
