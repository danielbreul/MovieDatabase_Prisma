using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IMovieService
    {
        /// <summary>
        ///     Retrieves a collection of movies as data transfer objects.
        /// </summary>
        /// <param name="title">Optional filter to search for movies by title. When provided, only movies matching the title are returned.</param>
        /// <returns>A <see cref="Result{T}"/> containing an enumerable collection of <see cref="MovieDto"/> objects if any
        /// movies are found; otherwise, a failed result with a <see cref="ResultState.NotFound"/> state.</returns>
        public Result<IEnumerable<MovieDto>> GetMovies(string? title = null);

        /// <summary>
        ///     Retrieves movie details by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to retrieve.</param>
        /// <returns>A <see cref="Result{T}"/> containing a <see cref="MovieDetailDto"/> if the movie is found; otherwise,
        /// a failed result with a <see cref="ResultState.NotFound"/> state.</returns>
        public Result<MovieDetailDto?> GetMovie(int id);

        /// <summary>
        ///     Creates a new movie using the specified data transfer object.
        /// </summary>
        /// <param name="movie">The data transfer object containing the information required to create a movie. The Title property must not
        /// be null, empty, or whitespace.</param>
        /// <returns>A <see cref="Result{T}"/> containing the created movie as a <see cref="MovieDetailDto"/> if the operation succeeds; otherwise,
        /// a failed result with a <see cref="ResultState.InvalidDto"/> state.</returns>
        public Result<MovieDetailDto?> CreateMovie(MovieCreateUpdateDto movie);

        /// <summary>
        ///     Updates the details of an existing movie with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to update.</param>
        /// <param name="movie">An object containing the updated movie information. The Title property must not be null, empty, or
        /// whitespace.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the update operation. Returns a result with state <see cref="ResultState.NotFound"/>
        /// if the movie is not found, or <see cref="ResultState.InvalidDto"/> if the provided data is invalid.</returns>
        public Result UpdateMovie(int id, MovieCreateUpdateDto movie);

        /// <summary>
        ///     Deletes the movie with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to delete.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the delete operation. Returns a result with state <see
        /// cref="ResultState.NotFound"/> if the movie does not exist.</returns>
        public Result DeleteMovie(int id);
    }
}
