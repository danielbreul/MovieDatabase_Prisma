using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IGenreRepository
    {
        public IEnumerable<Genre> GetGenresByName(IEnumerable<string> names);
        public Genre AddGenre(Genre genre);
        public void RemoveGenres(IEnumerable<Genre> genres);
    }
}
