using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IPersonService
    {
        /// <summary>
        ///     Asynchronously retrieves a collection of persons from the backend service.
        /// </summary>
        /// <remarks>If the backend service is unavailable or an error occurs during the request, the
        /// method logs the error and returns an empty collection. The returned collection may be empty, but is never
        /// null.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see
        /// cref="PersonViewModel"/> objects. Returns an empty collection if no persons are found or if the backend
        /// returns no content.</returns>
        public Task<IEnumerable<PersonViewModel>> GetPersonsAsync();

        /// <summary>
        ///     Creates a new person asynchronously and returns the created person if successful.
        /// </summary>
        /// <remarks>If the provided person data is invalid, the method logs the error and returns <see
        /// langword="null"/>. Network or server errors are also logged, and the method returns <see langword="null"/>
        /// in these cases.</remarks>
        /// <param name="person">The person data to create. The <see cref="PersonViewModel"/> must contain valid information for the new
        /// person. Cannot be null.</param>
        /// <returns>A <see cref="PersonViewModel"/> representing the created person if the operation succeeds; otherwise, <see
        /// langword="null"/>.</returns>
        public Task<PersonViewModel?> CreatePersonAsync(PersonViewModel genre);

        /// <summary>
        ///     Asynchronously updates the details of an existing person in the data store.
        /// </summary>
        /// <remarks>Returns <see langword="false"/> if the person does not exist or if the update fails
        /// due to invalid data or connectivity issues. The method logs errors using the application's error
        /// service.</remarks>
        /// <param name="person">A view model containing the updated information for the person. The person's identifier must correspond to
        /// an existing record.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the update
        /// was successful; otherwise, <see langword="false"/>.</returns>
        public Task<bool> UpdatePersonAsync(PersonViewModel genre);

        /// <summary>
        ///     Asynchronously deletes the person with the specified identifier.
        /// </summary>
        /// <remarks>If the person does not exist or cannot be deleted due to related references, the
        /// method returns <see langword="false"/> and logs an appropriate message. This method does not throw
        /// exceptions for HTTP errors; instead, it logs errors and returns <see langword="false"/>.</remarks>
        /// <param name="id">The unique identifier of the person to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the person
        /// was successfully deleted; otherwise, <see langword="false"/>.</returns>
        public Task<bool> DeletePersonAsync(int id);

        /// <summary>
        ///     Asynchronously retrieves a collection of movies directed by the specified person.
        /// </summary>
        /// <param name="id">The unique identifier of the person whose directed movies are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of movie view
        /// models representing the movies directed by the specified person. The collection is empty if no movies are
        /// found.</returns>
        public Task<IEnumerable<MovieViewModel>> GetDirectedMoviesAsync(int id);

        /// <summary>
        ///     Asynchronously retrieves the collection of movies written by the specified person.
        /// </summary>
        /// <param name="id">The unique identifier of the person whose written movies are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of movie view
        /// models representing the movies written by the specified person. The collection is empty if no movies are
        /// found.</returns>
        public Task<IEnumerable<MovieViewModel>> GetWrittenMoviesAsync(int id);

        /// <summary>
        ///     Asynchronously retrieves the collection of movies in which the specified person has acted.
        /// </summary>
        /// <param name="id">The unique identifier of the person whose acted-in movies are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see
        /// cref="MovieViewModel"/> objects representing the movies the person has acted in. The collection is empty if
        /// the person has not acted in any movies.</returns>
        public Task<IEnumerable<MovieViewModel>> GetActedInMoviesAsync(int id);

    }
}
