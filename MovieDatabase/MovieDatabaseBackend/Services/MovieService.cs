using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Repositories;

namespace MovieDatabaseBackend.Services
{
    public class MovieService(IMovieRepository repository) : IMovieService
    {
        private readonly IMovieRepository _repository = repository;

        public IEnumerable<MovieDto> GetMovies(string? title = null)
        {
            return _repository.GetMovies(title).Select(x => new MovieDto(x.Id, x.Title));
        }

        public MovieDetailDto GetMovie(int id)
        {
            var movie = _repository.GetMovie(id);
            return new MovieDetailDto(movie.Id, movie.Title)
            {
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                AgeRating = movie.AgeRating,
                Genres = movie.Genres.Select(g => new GenreDto(g.Id, g.Name)),
                Directors = movie.Directors.Select(d => new PersonDto(d.Id, d.Name)),
                Writer = movie.Writer is not null ? new PersonDto(movie.Writer.Id, movie.Writer.Name) : null,
                LeadActors = movie.LeadActors.Select(a => new PersonDto(a.Id, a.Name)),
                Duration = movie.Duration
            };
        }

        public void CreateMovie(MovieCreateUpdateDto movie)
        {


            var movie = _repository.GetMovie(id);
            return new MovieDetailDto(movie.Id, movie.Title)
            {
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                AgeRating = movie.AgeRating,
                Genres = movie.Genres.Select(g => new GenreDto(g.Id, g.Name)),
                Directors = movie.Directors.Select(d => new PersonDto(d.Id, d.Name)),
                Writer = movie.Writer is not null ? new PersonDto(movie.Writer.Id, movie.Writer.Name) : null,
                LeadActors = movie.LeadActors.Select(a => new PersonDto(a.Id, a.Name)),
                Duration = movie.Duration
            };
        }
    }
}
