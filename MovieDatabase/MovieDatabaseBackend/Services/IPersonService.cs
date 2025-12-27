using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Services
{
    public interface IPersonService
    {
        /// <summary>
        ///     Retrieves a collection of persons as data transfer objects.
        /// </summary>
        /// <returns>A <see cref="Result{T}"/> containing an enumerable collection of <see cref="PersonDto"/> objects if any
        /// persons are found; otherwise, a failed result with a <see cref="ResultState.NotFound"/> state.</returns>
        public Result<IEnumerable<PersonDto>> GetPersons();

        /// <summary>
        ///     Creates a new person using the specified data transfer object.
        /// </summary>
        /// <param name="personDto">The data transfer object containing the information required to create a person. The Name property must not
        /// be null, empty, or whitespace.</param>
        /// <returns>A <see cref="Result{T}"/> containing the created person as a <see cref="PersonDto"/> if the operation succeeds; otherwise, a failed result
        /// with a <see cref="ResultState.InvalidDto"/> state.</returns>
        public Result<PersonDto?> CreatePerson(PersonCreateUpdateDto person);

        /// <summary>
        ///     Updates the details of an existing person with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person to update.</param>
        /// <param name="personDto">An object containing the updated person information. The Name property must not be null, empty, or
        /// whitespace.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the update operation. Returns a result with state <see cref="ResultState.NotFound"/>
        /// if the person is not found, or <see cref="ResultState.InvalidDto"/> if the provided data is invalid.</returns>
        public Result UpdatePerson(int id, PersonCreateUpdateDto person);

        /// <summary>
        ///     Deletes the person with the specified identifier if they are not referenced by any movies as a director,
        /// writer, or actor.
        /// </summary>
        /// <param name="id">The unique identifier of the person to delete.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the delete operation. Returns a result with state <see
        /// cref="ResultState.NotFound"/> if the person does not exist, or <see cref="ResultState.Referenced"/> if the
        /// person is referenced by any movies.</returns>
        public Result DeletePerson(int id);

        /// <summary>
        ///     Retrieves a collection of movies directed by the specified person.
        /// </summary>
        /// <param name="personId">The unique identifier of the person whose directed movies are to be retrieved.</param>
        /// <returns>A <see cref="Result"/> containing a collection of movies directed by the specified person. If the person does not exist or
        /// has not directed any movies, the result has the <see cref="ResultState.NotFound"/> state.</returns>
        public Result<IEnumerable<MovieDto>> GetDirectedMovies(int personId);

        /// <summary>
        ///     Retrieves a collection of movies in which the specified person has acted.
        /// </summary>
        /// <param name="personId">The unique identifier of the person whose acted-in movies are to be retrieved.</param>
        /// <returns>A <see cref="Result"/> containing a collection of movies the person has acted in, or a failure result with 
        /// <see cref="ResultState.NotFound"/> state if the person does not exist or has not acted in any movies.</returns>
        public Result<IEnumerable<MovieDto>> GetActedInMovies(int personId);

        /// <summary>
        ///     Retrieves a collection of movies written by the specified person.
        /// </summary>
        /// <param name="personId">The unique identifier of the person whose written movies are to be retrieved.</param>
        /// <returns>A <see cref="Result"/> containing a collection of movies written by the specified person. If the person does not exist or
        /// has not written any movies, the result has the <see cref="ResultState.NotFound"/> state.</returns>
        public Result<IEnumerable<MovieDto>> GetWrittenMovies(int personId);
    }
}
