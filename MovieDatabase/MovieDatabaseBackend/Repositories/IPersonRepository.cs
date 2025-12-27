using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IPersonRepository
    {
        public IQueryable<Person> GetPersons();
        public IQueryable<Person> GetPersonsByIds(IEnumerable<int> ids);
        public Person? GetPerson(int id);
        public Person AddPerson(Person person);
        public void RemovePerson(Person person);
    }
}
