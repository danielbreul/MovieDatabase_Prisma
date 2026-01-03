using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<MovieViewModel>> GetMoviesAsync();
        public Task<MovieDetailViewModel?> GetMovieDetailAsync(MovieViewModel movie);
    }
}
