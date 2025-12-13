using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies(string? title);
        Movie GetMovie(int id);
    }
}
