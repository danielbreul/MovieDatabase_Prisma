using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<MovieViewModel>> GetMoviesAsync(string? title = null);
        public Task<MovieDetailViewModel?> GetMovieDetailAsync(int id);
        public Task<MovieViewModel?> CreateMovieAsync(MovieDetailViewModel movie);
        public Task<bool> UpdateMovieAsync(MovieDetailViewModel movie);
        public Task<bool> DeleteMovieAsync(int id);
    }
}