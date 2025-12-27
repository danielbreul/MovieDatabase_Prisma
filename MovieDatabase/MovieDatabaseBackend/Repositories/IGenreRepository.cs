using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IGenreRepository
    {
        public IQueryable<Genre> GetGenres();
        public IQueryable<Genre> GetGenresByIds(IEnumerable<int> ids);
        public Genre? GetGenre(int id);
        public Genre AddGenre(Genre genre);
        public void RemoveGenre(Genre genre);
    }
}
