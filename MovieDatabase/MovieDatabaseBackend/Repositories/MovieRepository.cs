using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public class MovieRepository(MovieDbContext context) : IMovieRepository
    {
        private readonly MovieDbContext _context = context;

        public IEnumerable<Movie> GetMovies(string? title = null)
        {
            var query = _context.Movies
                .Where(m => title == null || m.Title.Contains(title));
            return query.ToList();
        }

        public Movie GetMovie(int id)
        {
            return _context.Movies
                .First(m => m.Id == id);
        }

        public void CreateMovie(MovieCreateUpdateDto movie)
        {
            _context.Movies.Add(new Movie
            {
                Title = movie.Title,
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                AgeRating = movie.AgeRating,
                Genres = movie.Genres.ToList(),
                Directors = movie.Directors.ToList(),
                Writer = movie.Writer,
                LeadActors = movie.LeadActors.ToList(),
                Duration = movie.Duration
            });
        }
    }
}
