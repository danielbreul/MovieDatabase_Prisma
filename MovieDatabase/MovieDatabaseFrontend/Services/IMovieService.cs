using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<MovieViewModel>> GetMoviesAsync();
        public Task<MovieDetailViewModel?> GetMovieDetailAsync(MovieViewModel movie);
        public Task<MovieDetailViewModel?> CreateMovieAsync(MovieDetailViewModel movie);
        public Task UpdateMovieAsync(MovieDetailViewModel movie);
        public Task DeleteMovieAsync(MovieViewModel movie);
    }
}
