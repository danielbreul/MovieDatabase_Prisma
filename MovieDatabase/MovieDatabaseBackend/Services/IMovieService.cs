using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Common;

namespace MovieDatabaseBackend.Services
{
    public interface IMovieService
    {
        /// <summary>
        ///     Retrieves a collection of movies, optionally filtered by title.
        /// </summary>
        /// <param name="title">The title to filter movies by. If null or empty, all movies are returned.</param>
        /// <returns>An enumerable collection of <see cref="MovieDto"/> objects that match the specified title. If no movies
        /// match, the collection is empty.</returns>
        IEnumerable<MovieDto> GetMovies(string? title = null);

        /// <summary>
        ///     Retrieves detailed information for a movie with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to retrieve. Must be a positive integer.</param>
        /// <returns>A <see cref="MovieDetailDto"/> containing the details of the movie if found; otherwise, <see
        /// langword="null"/>.</returns>
        MovieDetailDto? GetMovie(int id);

        /// <summary>
        ///     Creates a new movie using the specified details.
        /// </summary>
        /// <param name="movie">An object containing the information required to create the movie. Cannot be <see langword="null"/>.</param>
        /// <returns>A result containing the details of the created movie if the operation succeeds; otherwise, a result
        /// indicating the failure reason. The value is <see langword="null"/> if the movie could not be created.</returns>
        Result<MovieDetailDto?> CreateMovie(MovieCreateUpdateDto movie);

        /// <summary>
        ///     Updates the details of an existing movie with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to update.</param>
        /// <param name="movie">An object containing the updated movie information. Cannot be <see langword="null"/>.</param>
        /// <returns>true if the movie was found and updated successfully; otherwise, false.</returns>
        bool UpdateMovie(int id, MovieCreateUpdateDto movie);

        /// <summary>
        ///     Deletes the movie with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to delete.</param>
        /// <returns>true if the movie was found and deleted; otherwise, false.</returns>
        bool DeleteMovie(int id);
    }
}
