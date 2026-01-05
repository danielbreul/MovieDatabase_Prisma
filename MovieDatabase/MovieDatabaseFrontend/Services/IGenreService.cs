using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreViewModel>> GetGenresAsync();
        public Task<GenreViewModel?> CreateGenreAsync(GenreViewModel genre);
        public Task<bool> UpdateGenreAsync(GenreViewModel genre);
        public Task<bool> DeleteGenreAsync(GenreViewModel genre);
        public Task<IEnumerable<MovieViewModel>> GetMoviesAsync(int id);
    }
}
