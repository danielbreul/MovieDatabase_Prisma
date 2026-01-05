using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IMovieService
    {
        /// <summary>
        ///     Asynchronously retrieves a collection of movies, optionally filtered by title.
        /// </summary>
        /// <remarks>If a title is specified, only movies with titles matching the filter are returned.
        /// The method logs errors if the HTTP request fails or if the backend is unavailable.</remarks>
        /// <param name="title">The title to filter movies by. If null or empty, all movies are retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of movie view
        /// models. The collection is empty if no movies are found.</returns>
        public Task<IEnumerable<MovieViewModel>> GetMoviesAsync(string? title = null);

        /// <summary>
        ///     Asynchronously retrieves detailed information for a movie by its unique identifier.
        /// </summary>
        /// <remarks>If the movie does not exist or cannot be retrieved, the method returns null. Network
        /// or server errors are logged.</remarks>
        /// <param name="id">The unique identifier of the movie to retrieve details for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a MovieDetailViewModel with the
        /// movie details if found; otherwise, null.</returns>
        public Task<MovieDetailViewModel?> GetMovieDetailAsync(int id);

        /// <summary>
        ///     Creates a new movie by sending the specified movie details to the backend service asynchronously.
        /// </summary>
        /// <remarks>If the backend service returns a validation error, the error is logged and null is
        /// returned. Network or unexpected errors are also logged. Only the Id and Title of the created movie are
        /// returned in the result.</remarks>
        /// <param name="movie">The details of the movie to create. Cannot be null. The provided information is used to populate the new
        /// movie record.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a MovieViewModel for the created
        /// movie if the operation succeeds; otherwise, null.</returns>
        public Task<MovieViewModel?> CreateMovieAsync(MovieDetailViewModel movie);

        /// <summary>
        ///     Asynchronously updates the details of an existing movie in the backend data store.
        /// </summary>
        /// <remarks>If the specified movie does not exist, the method logs an error and returns <see
        /// langword="false"/>. If the movie data is invalid or a network error occurs, the method also returns <see
        /// langword="false"/> after logging the error.</remarks>
        /// <param name="movie">The movie details to update. Must contain a valid movie identifier and updated property values.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the update
        /// was successful; otherwise, <see langword="false"/>.</returns>
        public Task<bool> UpdateMovieAsync(MovieDetailViewModel movie);

        /// <summary>
        ///     Asynchronously deletes the movie with the specified identifier from the data store.
        /// </summary>
        /// <remarks>If the movie does not exist, the method returns <see langword="false"/>. Network or
        /// server errors will also result in a return value of <see langword="false"/>.</remarks>
        /// <param name="id">The unique identifier of the movie to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the movie
        /// was successfully deleted; otherwise, <see langword="false"/>.</returns>
        public Task<bool> DeleteMovieAsync(int id);
    }
}