using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public interface IPersonRepository
    {
        public Person? GetPersonByName(string names);
        public IEnumerable<Person> GetPersonsByName(IEnumerable<string> names);
        public Person AddPerson(Person person);
        public void RemovePersons(IEnumerable<Person> persons);
    }
}
