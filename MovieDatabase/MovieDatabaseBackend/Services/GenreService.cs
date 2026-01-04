using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Services
{
    public class GenreService(IUnitOfWork unitOfWork) : IGenreService
    {
        private readonly IUnitOfWork _uof = unitOfWork;

        public Result<IEnumerable<GenreDto>> GetGenres()
        {
            var genres = _uof.Genres.GetGenres();
            if (genres.Any())
            {
                return Result<IEnumerable<GenreDto>>.Ok(genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name }));
            }
            else
            {
                return Result<IEnumerable<GenreDto>>.Fail(ResultState.NotFound);
            }
        }

        public Result<GenreDto?> CreateGenre(GenreCreateUpdateDto genreDto)
        {
            // Name is required
            if (string.IsNullOrWhiteSpace(genreDto.Name))
            {
                return Result<GenreDto?>.Fail(ResultState.InvalidDto, "Name is required!");
            }

            var genre = new Genre
            {
                Name = genreDto.Name
            };

            _uof.Genres.AddGenre(genre);
            _uof.SaveChanges();

            return Result<GenreDto?>.Ok(new GenreDto { Id = genre.Id, Name = genre.Name });
        }

        public Result UpdateGenre(int id, GenreCreateUpdateDto genreDto)
        {
            // Name is required
            if (string.IsNullOrWhiteSpace(genreDto.Name))
            {
                return Result.Fail(ResultState.InvalidDto, "Name is required!");
            }

            var genre = _uof.Genres.GetGenre(id);
            if (genre is null)
            {
                return Result.Fail(ResultState.NotFound);
            }

            genre.Name = genreDto.Name;

            _uof.SaveChanges();
            return Result.Ok();
        }

        public Result DeleteGenre(int id)
        {
            var genre = _uof.Genres.GetGenre(id);
            if (genre is null)
            {
                return Result.Fail(ResultState.NotFound);
            }
            else if (genre.Movies.Count != 0)
            {
                return Result.Fail(ResultState.Referenced, "Cannot delete referenced genre!");
            }

            _uof.Genres.RemoveGenre(genre);
            _uof.SaveChanges();

            return Result.Ok();
        }

        public Result<IEnumerable<MovieDto>> GetMovies(int genreId)
        {
            var genre = _uof.Genres.GetGenre(genreId);
            if (genre is not null)
            {
                var movies = genre.Movies.Select(m => new MovieDto { Id = m.Id, Title = m.Title });
                if (movies.Any())
                {
                    return Result<IEnumerable<MovieDto>>.Ok(movies);
                }
            }
            return Result<IEnumerable<MovieDto>>.Fail(ResultState.NotFound);
        }
    }
}
