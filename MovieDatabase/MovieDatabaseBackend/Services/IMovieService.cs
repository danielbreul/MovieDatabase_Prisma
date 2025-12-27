using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IMovieService
    {
        public Result<IEnumerable<MovieDto>> GetMovies(string? title = null);

        public Result<MovieDetailDto?> GetMovie(int id);

        public Result<MovieDetailDto?> CreateMovie(MovieCreateUpdateDto movie);
        
        public Result UpdateMovie(int id, MovieCreateUpdateDto movie);

        public Result DeleteMovie(int id);
    }
}
