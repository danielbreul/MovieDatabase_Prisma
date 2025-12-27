using MovieDatabaseBackend.Repositories;

namespace MovieDatabaseBackend.Data
{
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        IGenreRepository Genres { get; }
        IPersonRepository Persons { get; }

        int SaveChanges();
    }
}
