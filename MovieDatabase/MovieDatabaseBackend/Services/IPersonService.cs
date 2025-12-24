using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IPersonService
    {
        public IEnumerable<MovieDto> GetDirectedMovies(int writerId);
        public IEnumerable<MovieDto> GetActedInMovies(int writerId);
        public IEnumerable<MovieDto> GetWrittenMovies(int writerId);
    }
}
