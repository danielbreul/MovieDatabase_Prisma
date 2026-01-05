using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IGenreService
    {
        /// <summary>
        ///     Asynchronously retrieves a collection of available genres from the backend service.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of
        /// genre view models. The collection is empty if no genres are available or if the request fails.</returns>
        public Task<IEnumerable<GenreViewModel>> GetGenresAsync();

        /// <summary>
        ///     Creates a new genre asynchronously using the provided genre information.
        /// </summary>
        /// <remarks>If the genre data is invalid or the backend service is unavailable, the method
        /// returns null and logs the error using the error service.</remarks>
        /// <param name="genre">A view model containing the details of the genre to create. The genre's name must be specified.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a view model of the created
        /// genre if the operation succeeds; otherwise, null.</returns>
        public Task<GenreViewModel?> CreateGenreAsync(GenreViewModel genre);
        
        /// <summary>
        ///     Asynchronously updates the specified genre in the data store.
        /// </summary>
        /// <remarks>Returns <see langword="false"/> if the genre does not exist or if the update fails
        /// due to invalid data or a network error. Error details may be logged using the error service.</remarks>
        /// <param name="genre">The genre to update, containing the updated values. Must not be null. The genre's Id property identifies the
        /// genre to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the update
        /// was successful; otherwise, <see langword="false"/>.</returns>
        public Task<bool> UpdateGenreAsync(GenreViewModel genre);

        /// <summary>
        ///     Asynchronously deletes the genre with the specified identifier.
        /// </summary>
        /// <remarks>Returns <see langword="false"/> if the genre does not exist or cannot be deleted due
        /// to existing references. Network or server errors will also result in a <see langword="false"/> return
        /// value.</remarks>
        /// <param name="id">The unique identifier of the genre to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the genre
        /// was successfully deleted; otherwise, <see langword="false"/>.</returns>
        public Task<bool> DeleteGenreAsync(int id);

        /// <summary>
        ///     Asynchronously retrieves a collection of movies associated with the specified genre.
        /// </summary>
        /// <param name="id">The unique identifier of the genre for which to retrieve movies.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of movie view
        /// models for the specified genre. Returns an empty collection if no movies are found.</returns>
        public Task<IEnumerable<MovieViewModel>> GetMoviesAsync(int id);
    }
}
