using Microsoft.EntityFrameworkCore;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public class GenreRepository(MovieDbContext context) : IGenreRepository
    {
        private readonly MovieDbContext _context = context;

        public IQueryable<Genre> GetGenres()
        {
            return _context.Genres
                .Include(p => p.Movies);
        }

        public IQueryable<Genre> GetGenresByIds(IEnumerable<int> ids)
        {
            return _context.Genres
                .Where(g => ids.Contains(g.Id));
        }

        public Genre? GetGenre(int id)
        {
            return _context.Genres
                .Include(g => g.Movies)
                .FirstOrDefault(g => g.Id == id);
        }

        public Genre AddGenre(Genre genre)
        {
            return _context.Genres.Add(genre).Entity;
        }

        public void RemoveGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
        }
    }
}
