using Microsoft.EntityFrameworkCore;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public class GenreRepository(MovieDbContext context) : IGenreRepository
    {
        private readonly MovieDbContext _context = context;

        public IEnumerable<Genre> GetGenresByName(IEnumerable<string> names)
        {
            return _context.Genres.Where(g => names.Contains(g.Name));
        }

        public Genre AddGenre(Genre genre)
        {
            return _context.Genres.Add(genre).Entity;
        }

        public void RemoveGenres(IEnumerable<Genre> genres)
        {
            _context.Genres.RemoveRange(genres);
        }
    }
}
