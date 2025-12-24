using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public class GenreService(IUnitOfWork unitOfWork) : IGenreService
    {
        private readonly IUnitOfWork _uof = unitOfWork;

        public IEnumerable<MovieDto> GetMovies(int genreId)
        {
            var genre = _uof.Genres.GetGenreById(genreId);
            if (genre is null)
            {
                return [];
            }
            else
            {
                var msg = "";
                genre.Movies.Select(g => " - Genre: " + g.Id + "; " + g.Title);
                Console.Write(msg);
                return genre.Movies.Select(m => new MovieDto(m.Id, m.Title));
            }
        }
    }
}
