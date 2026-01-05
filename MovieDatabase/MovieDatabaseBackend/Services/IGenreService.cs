using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IGenreService
    {
        /// <summary>
        ///     Retrieves a collection of genres as data transfer objects.
        /// </summary>
        /// <returns>A <see cref="Result{T}"/> containing an enumerable collection of <see cref="GenreDto"/> objects if any
        /// genres are found; otherwise, a failed result with a <see cref="ResultState.NotFound"/> state.</returns>
        public Result<IEnumerable<GenreDto>> GetGenres();

        /// <summary>
        ///     Creates a new genre using the specified data transfer object.
        /// </summary>
        /// <param name="genre">The data transfer object containing the information required to create a genre. The Name property must not
        /// be null, empty, or whitespace.</param>
        /// <returns>A <see cref="Result{T}"/> containing the created genre as a <see cref="GenreDto"/> if the operation succeeds; otherwise, a failed result
        /// with a <see cref="ResultState.InvalidDto"/> state.</returns>
        public Result<GenreDto?> CreateGenre(GenreCreateUpdateDto genre);

        /// <summary>
        ///     Updates the details of an existing genre with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to update.</param>
        /// <param name="genre">An object containing the updated genre information. The Name property must not be null, empty, or
        /// whitespace.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the update operation. Returns a result with state <see cref="ResultState.NotFound"/>
        /// if the genre is not found, or <see cref="ResultState.InvalidDto"/> if the provided data is invalid.</returns>
        public Result UpdateGenre(int id, GenreCreateUpdateDto genre);

        /// <summary>
        ///     Deletes the genre with the specified identifier if it is not referenced by any movies.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to delete.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the delete operation. Returns a result with state <see
        /// cref="ResultState.NotFound"/> if the genre does not exist, or <see cref="ResultState.Referenced"/> if the
        /// genre is referenced by any movies.</returns>
        public Result DeleteGenre(int id);

        /// <summary>
        ///     Retrieves a collection of movies associated with the specified genre.
        /// </summary>
        /// <param name="genreId">The unique identifier of the genre whose associated movies are to be retrieved.</param>
        /// <returns>A <see cref="Result{T}"/> containing a collection of movies associated with the specified genre. If the genre does not exist or
        /// has no associated movies, the result has the <see cref="ResultState.NotFound"/> state.</returns>
        public Result<IEnumerable<MovieDto>> GetMovies(int genreId);
    }
}
