using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IGenreRepository
    {
        /// <summary>
        ///     Retrieves all genres from the data store, including their associated movies.
        /// </summary>
        /// <remarks>The returned genres include their associated movies due to eager loading. The
        /// collection reflects the current state of the data store at the time of the call.</remarks>
        /// <returns>An enumerable collection of <see cref="Genre"/> objects, each populated with its related movies.</returns>
        public IEnumerable<Genre> GetGenres();

        /// <summary>
        ///     Retrieves a collection of genres that match the specified identifiers.
        /// </summary>
        /// <param name="ids">A collection of genre IDs to search for. Each ID should correspond to an existing genre.</param>
        /// <returns>An enumerable collection of <see cref="Genre"/> objects whose IDs are contained in <paramref name="ids"/>.
        /// If no genres match, the collection will be empty.</returns>
        public IEnumerable<Genre> GetGenresByIds(IEnumerable<int> ids);

        /// <summary>
        ///     Retrieves the genre with the specified identifier, including its associated movies.
        /// </summary>
        /// <remarks>The returned genre includes its related movies. This method performs a database query
        /// and may return null if the genre is not found.</remarks>
        /// <param name="id">The unique identifier of the genre to retrieve.</param>
        /// <returns>A <see cref="Genre"/> object representing the genre with the specified identifier, or null
        /// if no such genre exists.</returns>
        public Genre? GetGenre(int id);

        /// <summary>
        ///     Adds a new genre to the data context and returns the tracked entity.
        /// </summary>
        /// <remarks>The returned entity is tracked by the context and may have updated properties, such
        /// as generated keys, after being added.</remarks>
        /// <param name="genre">The genre to add to the context.</param>
        /// <returns>The tracked <see cref="Genre"/> entity after it has been added to the context.</returns>
        public Genre AddGenre(Genre genre);

        /// <summary>
        ///     Removes the specified genre from the data context.
        /// </summary>
        /// <param name="genre">The genre entity to remove from the context.</param>
        public void RemoveGenre(Genre genre);
    }
}
