using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IPersonRepository
    {
        /// <summary>
        ///     Retrieves all persons from the data store, including their associated movies.
        /// </summary>
        /// <remarks>The returned persons include their associated movies due to eager loading. The
        /// collection reflects the current state of the data store at the time of the call.</remarks>
        /// <returns>An enumerable collection of <see cref="Person"/> objects, each populated with its related movies.</returns>
        public IEnumerable<Person> GetPersons();

        /// <summary>
        ///     Retrieves a collection of persons that match the specified identifiers.
        /// </summary>
        /// <param name="ids">A collection of person IDs to search for. Each ID should correspond to an existing person.</param>
        /// <returns>An enumerable collection of <see cref="Person"/> objects whose IDs are contained in <paramref name="ids"/>.
        /// If no persons match, the collection will be empty.</returns>
        public IEnumerable<Person> GetPersonsByIds(IEnumerable<int> ids);

        /// <summary>
        ///     Retrieves the person with the specified identifier, including its associated movies.
        /// </summary>
        /// <remarks>The returned person includes its related movies. This method performs a database query
        /// and may return null if the person is not found.</remarks>
        /// <param name="id">The unique identifier of the person to retrieve.</param>
        /// <returns>A <see cref="Person"/> object representing the person with the specified identifier, or null
        /// if no such person exists.</returns>
        public Person? GetPerson(int id);

        /// <summary>
        ///     Adds a new person to the data context and returns the tracked entity.
        /// </summary>
        /// <remarks>The returned entity is tracked by the context and may have updated properties, such
        /// as generated keys, after being added.</remarks>
        /// <param name="person">The person to add to the context.</param>
        /// <returns>The tracked <see cref="Person"/> entity after it has been added to the context.</returns>
        public Person AddPerson(Person person);

        /// <summary>
        ///     Removes the specified person from the data context.
        /// </summary>
        /// <param name="person">The person entity to remove from the context.</param>
        public void RemovePerson(Person person);
    }
}
