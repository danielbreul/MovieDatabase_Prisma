using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Services
{
    public class MovieService(IUnitOfWork unitOfWork) : IMovieService
    {
        private readonly IUnitOfWork _uof = unitOfWork;

        public Result<IEnumerable<MovieDto>> GetMovies(string? title = null)
        {
            var movies = _uof.Movies.GetMovies()
                .Where(m => title == null || m.Title.ToLower().Contains(title.ToLower()));
            if (movies.Any())
            {
                return Result<IEnumerable<MovieDto>>.Ok(movies.Select(m => new MovieDto { Id = m.Id, Title = m.Title }));
            }
            else
            {
                return Result<IEnumerable<MovieDto>>.Fail(ResultState.NotFound);
            }
        }

        public Result<MovieDetailDto?> GetMovie(int id)
        {
            var movie = _uof.Movies.GetMovie(id);
            if (movie is not null)
            {
                return Result<MovieDetailDto?>.Ok(MovieToDetailDto(movie));
            }
            else
            {
                return Result<MovieDetailDto?>.Fail(ResultState.NotFound);
            }
        }

        public Result<MovieDetailDto?> CreateMovie(MovieCreateUpdateDto movieDto)
        {
            // Title is required
            if (string.IsNullOrWhiteSpace(movieDto.Title))
            {
                return Result<MovieDetailDto?>.Fail(ResultState.InvalidDto, "Title is required!");
            }

            var movie = new Movie
            {
                Title = movieDto.Title,
                Plot = movieDto.Plot,
                ReleaseDate = movieDto.ReleaseDate,
                Rating = movieDto.Rating,
                AgeRating = movieDto.AgeRating,
                Duration = movieDto.Duration,
            };

            var errorMessage = TransferGenreAndPersonsToEntity(movieDto, movie);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return Result<MovieDetailDto?>.Fail(ResultState.InvalidDto, errorMessage);
            }

            _uof.Movies.AddMovie(movie);
            _uof.SaveChanges();

            return Result<MovieDetailDto?>.Ok(MovieToDetailDto(movie));
        }

        public Result UpdateMovie(int id, MovieCreateUpdateDto movieDto)
        {
            // Title is required
            if (string.IsNullOrWhiteSpace(movieDto.Title))
            {
                return Result.Fail(ResultState.InvalidDto, "Title is required!");
            }

            var movie = _uof.Movies.GetMovie(id);
            if (movie is null)
            {
                return Result.Fail(ResultState.NotFound);
            }

            movie.Title = movieDto.Title;
            movie.Plot = movieDto.Plot;
            movie.ReleaseDate = movieDto.ReleaseDate;
            movie.Rating = movieDto.Rating;
            movie.AgeRating = movieDto.AgeRating;
            movie.Duration = movieDto.Duration;

            var errorMessage = TransferGenreAndPersonsToEntity(movieDto, movie);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return Result<MovieDetailDto?>.Fail(ResultState.InvalidDto, errorMessage);
            }

            _uof.SaveChanges();
            return Result.Ok();
        }

        public Result DeleteMovie(int id)
        {
            var movie = _uof.Movies.GetMovie(id);
            if (movie is null)
            {
                return Result.Fail(ResultState.NotFound);
            }

            _uof.Movies.RemoveMovie(movie);
            _uof.SaveChanges();

            return Result.Ok();
        }

        /// <summary>
        ///     Creates a new MovieDetailDto instance that represents the specified movie, including its details, genres,
        /// directors, writer, and lead actors.
        /// </summary>
        /// <param name="movie">The Movie object to convert to a MovieDetailDto. Cannot be null.</param>
        /// <returns>A MovieDetailDto containing detailed information about the specified movie.</returns>
        private static MovieDetailDto MovieToDetailDto(Movie movie)
        {
            return new MovieDetailDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                AgeRating = movie.AgeRating,
                Genres = movie.Genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name }),
                Directors = movie.Directors.Select(p => new PersonDto { Id = p.Id, Name = p.Name }),
                Writer = movie.Writer is not null ? new PersonDto { Id = movie.Writer.Id, Name = movie.Writer.Name } : null,
                Actors = movie.Actors.Select(p => new PersonDto { Id = p.Id, Name = p.Name }),
                Duration = movie.Duration
            };
        }

        /// <summary>
        ///     Validates and transfers genre and person information from a movie data transfer object to a movie entity.
        /// </summary>
        /// <remarks>This method does not persist changes to the database. It only updates the provided
        /// movie entity with validated genres, directors, actors, and writer based on the identifiers in the data
        /// transfer object. Callers should check the return value for validation errors before proceeding with further
        /// operations.</remarks>
        /// <param name="movieDto">The data transfer object containing genre and person identifiers to be validated and assigned to the movie
        /// entity.</param>
        /// <param name="movie">The movie entity to which validated genres and persons will be assigned.</param>
        /// <returns>A string containing an error message if any genre or person identifiers in the data transfer object do not
        /// exist; otherwise, null if all identifiers are valid and the transfer is successful.</returns>
        private string? TransferGenreAndPersonsToEntity(MovieCreateUpdateDto movieDto, Movie movie)
        {
            // Validate genre ids
            var genreIds = movieDto.Genres.ToHashSet();
            var genres = _uof.Genres.GetGenresByIds(genreIds);
            var genresNotFound = genreIds.Where(id => !genres.Any(g => g.Id == id));

            var message = string.Empty;
            if (genresNotFound.Any())
            {
                message += "Some genre IDs do not exist: "
                    + string.Join(", ", genresNotFound) + "; ";
            }

            // Validate person ids
            var personIds = movieDto.Directors.Concat(movieDto.Actors).ToHashSet();
            if (movieDto.Writer is not null)
            {
                personIds.Add(movieDto.Writer.Value);
            }
            var persons = _uof.Persons.GetPersonsByIds(personIds); // Fetch all persons in one query
            var personsNotFound = personIds.Where(id => !persons.Any(p => p.Id == id));

            if (personsNotFound.Any())
            {
                message += "Some person IDs do not exist: "
                    + string.Join(", ", personsNotFound) + "; ";
            }

            // Check for errors
            if (!string.IsNullOrWhiteSpace(message))
            {
                return message;
            }

            // Transfer genres and persons to movie entity
            movie.Genres = genres.ToList();
            movie.Directors = persons.Where(p => movieDto.Directors.Contains(p.Id)).ToList();
            movie.Actors = persons.Where(p => movieDto.Actors.Contains(p.Id)).ToList();
            if (movieDto.Writer is not null)
            {
                movie.Writer = persons.First(p => p.Id == movieDto.Writer.Value);
            }

            return null;
        }

    }
}
