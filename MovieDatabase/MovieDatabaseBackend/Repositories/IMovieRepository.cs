using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IMovieRepository
    {
        public IQueryable<Movie> GetMovies(string? title);
        public Movie? GetMovie(int id);
        public Movie AddMovie(Movie movie);
        public void RemoveMovie(Movie movie);
    }
}
