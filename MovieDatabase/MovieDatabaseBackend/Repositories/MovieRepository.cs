using Microsoft.EntityFrameworkCore;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public class MovieRepository(MovieDbContext context) : IMovieRepository
    {
        private readonly MovieDbContext _context = context;

        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Directors)
                .Include(m => m.Actors)
                .Include(m => m.Writer);
        }

        public Movie? GetMovie(int id)
        {
            return _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Directors)
                .Include(m => m.Actors)
                .Include(m => m.Writer)
                .FirstOrDefault(m => m.Id == id);
        }

        public Movie AddMovie(Movie movie)
        {
            return _context.Movies.Add(movie).Entity;
        }

        public void RemoveMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
        }
    }
}
