using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<Movie>> GetMoviesAsync();
    }
}
