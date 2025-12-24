using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Entities;
using MovieDatabaseBackend.Common;

namespace MovieDatabaseBackend.Services
{
    public class MovieService(IUnitOfWork unitOfWork) : IMovieService
    {
        private readonly IUnitOfWork _uof = unitOfWork;

        public IEnumerable<MovieDto> GetMovies(string? title = null)
        {
            return _uof.Movies.GetMovies(title).Select(x => new MovieDto(x.Id, x.Title));
        }

        public MovieDetailDto? GetMovie(int id)
        {
            var movie = _uof.Movies.GetMovie(id);
            if (movie is not null)
            {
                return MovieToDetailDto(movie);
            }
            else
            {
                return null;
            }
        }

        public Result<MovieDetailDto?> CreateMovie(MovieCreateUpdateDto movieDto)
        {
            // Title is required
            if (string.IsNullOrWhiteSpace(movieDto.Title))
            {
                return Result<MovieDetailDto?>.Fail("Title is required!");
            }

            var movie = new Movie
            {
                Title = movieDto.Title,
                Plot = movieDto.Plot,
                ReleaseDate = movieDto.ReleaseDate,
                Rating = movieDto.Rating,
                AgeRating = movieDto.AgeRating,
                Duration = movieDto.Duration,
                Genres = GetOrCreateGenres(movieDto.Genres),
                Directors = GetOrCreatePersons(movieDto.Directors),
                LeadActors = GetOrCreatePersons(movieDto.LeadActors),
                Writer = GetOrCreatePerson(movieDto.Writer)
            };

            _ = _uof.Movies.AddMovie(movie);
            _uof.SaveChanges();

            return Result<MovieDetailDto?>.Ok(MovieToDetailDto(movie));
        }

        public bool UpdateMovie(int id, MovieCreateUpdateDto movieDto)
        {
            var movie = _uof.Movies.GetMovie(id);
            if (movie is null)
            {
                return false;
            }

            movie.Title = movieDto.Title;
            movie.Plot = movieDto.Plot;
            movie.ReleaseDate = movieDto.ReleaseDate;
            movie.Rating = movieDto.Rating;
            movie.AgeRating = movieDto.AgeRating;
            movie.Duration = movieDto.Duration;

            // Genres
            var newGenres = GetOrCreateGenres(movieDto.Genres);
            var genresToRemove = movie.Genres
                .Where(g => newGenres.All(ng => ng.Id != g.Id));
            foreach (var genre in genresToRemove)
            {
                genre.Movies.Remove(movie);
            }
            movie.Genres = newGenres;

            // Directors
            var newDirectors = GetOrCreatePersons(movieDto.Directors);
            var directorsToRemove = movie.Directors
                .Where(p => newDirectors.All(np => np.Id != p.Id));
            foreach (var director in directorsToRemove)
            {
                director.DirectedMovies.Remove(movie);
            }
            movie.Directors = newDirectors;

            // Lead actors
            var newLeadActors = GetOrCreatePersons(movieDto.LeadActors);
            var leadActorsToRemove = movie.LeadActors
                .Where(p => newLeadActors.All(np => np.Id != p.Id));
            foreach (var actor in leadActorsToRemove)
            {
                actor.ActedInMovies.Remove(movie);
            }
            movie.LeadActors = newLeadActors;

            // Persons that might be orphaned
            var personsToRemove = directorsToRemove.Concat(leadActorsToRemove);

            // Writer
            var oldWriter = movie.Writer;
            movie.Writer = GetOrCreatePerson(movieDto.Writer);
            if (oldWriter != null && oldWriter.Id != movie.Writer?.Id)
            {
                oldWriter.WrittenMovies.Remove(movie);
                personsToRemove = personsToRemove.Append(oldWriter);
            }

            // Remove unused entities
            RemoveUnusedGenres(genresToRemove);
            RemoveUnusedPersons(personsToRemove);

            _uof.SaveChanges();
            return true;
        }

        public bool DeleteMovie(int id)
        {
            var movie = _uof.Movies.GetMovie(id);
            if (movie is null)
            {
                return false;
            }

            // Genres
            foreach (var genre in movie.Genres)
            {
                genre.Movies.Remove(movie);
            }

            // Persons that might be orphaned
            List<Person> personsToRemove = [];

            // Directors
            foreach (var director in movie.Directors)
            {
                director.ActedInMovies.Remove(movie);
            }
            personsToRemove.AddRange(movie.Directors);

            // LeadActors
            foreach (var actor in movie.LeadActors)
            {
                actor.ActedInMovies.Remove(movie);
            }
            personsToRemove.AddRange(movie.LeadActors);

            // Writer
            if (movie.Writer is not null)
            {
                movie.Writer.WrittenMovies.Remove(movie);
                personsToRemove.Add(movie.Writer);
            }

            // Remove unused entities
            RemoveUnusedPersons(personsToRemove);
            RemoveUnusedGenres(movie.Genres);

            _uof.Movies.RemoveMovie(movie);
            _uof.SaveChanges();

            return true;
        }

        /// <summary>
        ///     Creates a new MovieDetailDto instance that represents the specified movie, including its details, genres,
        /// directors, writer, and lead actors.
        /// </summary>
        /// <param name="movie">The Movie object to convert to a MovieDetailDto. Cannot be null.</param>
        /// <returns>A MovieDetailDto containing detailed information about the specified movie.</returns>
        private static MovieDetailDto MovieToDetailDto(Movie movie)
        {
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

        /// <summary>
        ///     Retrieves existing genres by name or creates new genres for any names that do not already exist.
        /// </summary>
        /// <remarks>Duplicate names in the input are processed only once. Leading and trailing whitespace
        /// in names is trimmed before processing.</remarks>
        /// <param name="names">A collection of genre names to retrieve or create. Names that are null, empty, or whitespace are ignored.</param>
        /// <returns>A list of genres corresponding to the provided names. If a genre does not exist, it is created and included
        /// in the returned list.</returns>
        private List<Genre> GetOrCreateGenres(IEnumerable<string> names)
        {
            var nameList = names
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .ToList();

            var existingGenres = _uof.Genres.GetGenresByName(nameList).ToList();
            var missingGenreNames = nameList.Except(existingGenres.Select(p => p.Name));
            foreach (var genre in missingGenreNames)
            {
                existingGenres.Add(_uof.Genres.AddGenre(new Genre { Name = genre }));
            }
            return existingGenres;
        }

        /// <summary>
        ///     Retrieves an existing person by name or creates a new person if none exists.
        /// </summary>
        /// <param name="name">The name of the person to retrieve or create. Leading and trailing whitespace is ignored. If null or
        /// whitespace, the method returns null.</param>
        /// <returns>A <see cref="Person"/> object representing the existing or newly created person, or null if <paramref
        /// name="name"/> is null or consists only of whitespace.</returns>
        private Person? GetOrCreatePerson(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            else
            {
                name = name.Trim();
                return _uof.Persons.GetPersonByName(name) ?? _uof.Persons.AddPerson(new Person { Name = name });
            }
        }

        /// <summary>
        ///     Retrieves existing persons matching the specified names, or creates new persons for any names not already present.
        /// </summary>
        /// <param name="names">A collection of person names to retrieve or create. Names that are null, empty, or whitespace are ignored.</param>
        /// <returns>A list of persons corresponding to the provided names. If a name does not match an existing person, a new
        /// person is created and included in the result.</returns>
        private List<Person> GetOrCreatePersons(IEnumerable<string> names)
        {
            var nameList = names
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .ToList();

            var existingPersons = _uof.Persons.GetPersonsByName(nameList).ToList();
            var missingPersonNames = nameList.Except(existingPersons.Select(p => p.Name));
            foreach (var person in missingPersonNames)
            {
                existingPersons.Add(_uof.Persons.AddPerson(new Person { Name = person }));
            }
            return existingPersons;
        }

        /// <summary>
        ///     Removes genres from the repository that are not associated with any movies.
        /// </summary>
        /// <remarks>Duplicate genres in the input collection are handled by their unique identifier.</remarks>
        /// <param name="genres">A collection of genres to evaluate for removal. Each genre in the collection is checked for associations
        /// with movies.</param>
        private void RemoveUnusedGenres(IEnumerable<Genre> genres)
        {
            var deletable = genres
                .GroupBy(g => g.Id) // deduplicate
                .Select(gp => gp.First())
                .Where(g => g.Movies.Count == 0)
                .ToList();

            if (deletable.Count > 0)
            {
                _uof.Genres.RemoveGenres(deletable);
            }
        }

        /// <summary>
        ///     Removes persons from the repository who have not acted in, written, or directed any movies.
        /// </summary>
        /// <remarks>Duplicate persons in the input collection are handled by their unique identifier.</remarks>
        /// <param name="persons">The collection of persons to evaluate for removal. Each person is removed if they have no associated acting,
        /// writing, or directing credits.</param>
        private void RemoveUnusedPersons(IEnumerable<Person> persons)
        {
            var deletable = persons
                .GroupBy(p => p.Id) // deduplicate
                .Select(g => g.First())
                .Where(p => p.ActedInMovies.Count == 0 && p.WrittenMovies.Count == 0 && p.DirectedMovies.Count == 0)
                .ToList();

            if (deletable.Count > 0)
            {
                _uof.Persons.RemovePersons(deletable);
            }
        }

    }
}
