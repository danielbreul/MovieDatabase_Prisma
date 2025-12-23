using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public class PersonRepository(MovieDbContext context) : IPersonRepository
    {
        private readonly MovieDbContext _context = context;

        public Person? GetPersonByName(string names)
        {
            return _context.Persons.FirstOrDefault(p => names.Contains(p.Name));
        }

        public IEnumerable<Person> GetPersonsByName(IEnumerable<string> names)
        {
            return _context.Persons.Where(p => names.Contains(p.Name));
        }

        public Person AddPerson(Person person)
        {
            return _context.Persons.Add(person).Entity;
        }

        public void RemovePersons(IEnumerable<Person> persons)
        {
            _context.Persons.RemoveRange(persons);
        }
    }
}
