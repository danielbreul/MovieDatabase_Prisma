using MovieDatabaseBackend.Repositories;

namespace MovieDatabaseBackend.Data
{
    public class UnitOfWork(MovieDbContext context) : IUnitOfWork
    {
        private readonly MovieDbContext _context = context;

        public IMovieRepository Movies { get; } = new MovieRepository(context);
        public IGenreRepository Genres { get; } = new GenreRepository(context);
        public IPersonRepository Persons { get; } = new PersonRepository(context);

        public int SaveChanges() => _context.SaveChanges();
    }
}
