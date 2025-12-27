using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IGenreService
    {
        public Result<IEnumerable<GenreDto>> GetGenres();

        public Result<GenreDto?> CreateGenre(GenreCreateUpdateDto genre);

        public Result UpdateGenre(int id, GenreCreateUpdateDto genre);

        public Result DeleteGenre(int id);

        public Result<IEnumerable<MovieDto>> GetMovies(int genreId);
    }
}
