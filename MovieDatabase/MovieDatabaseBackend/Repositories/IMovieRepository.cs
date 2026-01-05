using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IMovieRepository
    {
        /// <summary>
        ///     Retrieves all movies from the data store, including their associated genres and persons.
        /// </summary>
        /// <remarks>The returned movies include their associated movies due to eager loading. The
        /// collection reflects the current state of the data store at the time of the call.</remarks>
        /// <returns>An enumerable collection of <see cref="Movie"/> objects, each populated with its related movies.</returns>
        public IEnumerable<Movie> GetMovies();

        /// <summary>
        ///     Retrieves the movie with the specified identifier, including its associated genres and persons.
        /// </summary>
        /// <remarks>The returned movie includes its related genres and persons. This method performs a database query
        /// and may return null if the movie is not found.</remarks>
        /// <param name="id">The unique identifier of the movie to retrieve.</param>
        /// <returns>A <see cref="Movie"/> object representing the movie with the specified identifier, or null
        /// if no such movie exists.</returns>
        public Movie? GetMovie(int id);

        /// <summary>
        ///     Adds a new movie to the data context and returns the tracked entity.
        /// </summary>
        /// <remarks>The returned entity is tracked by the context and may have updated properties, such
        /// as generated keys, after being added.</remarks>
        /// <param name="movie">The movie to add to the context.</param>
        /// <returns>The tracked <see cref="Movie"/> entity after it has been added to the context.</returns>
        public Movie AddMovie(Movie movie);

        /// <summary>
        ///     Removes the specified movie from the data context.
        /// </summary>
        /// <param name="movie">The movie entity to remove from the context.</param>
        public void RemoveMovie(Movie movie);
    }
}
