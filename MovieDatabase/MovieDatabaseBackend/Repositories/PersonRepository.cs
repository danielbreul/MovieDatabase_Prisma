using Microsoft.EntityFrameworkCore;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public class PersonRepository(MovieDbContext context) : IPersonRepository
    {
        private readonly MovieDbContext _context = context;

        public Person? GetPersonByName(string name)
        {
            return _context.Persons
                .Include(p => p.DirectedMovies)
                .Include(p => p.ActedInMovies)
                .Include(p => p.WrittenMovies)
                .FirstOrDefault(p => name.Trim() == p.Name);
        }

        public IEnumerable<Person> GetPersonsByName(IEnumerable<string> names)
        {
            return _context.Persons
                .Include(p => p.DirectedMovies)
                .Include(p => p.ActedInMovies)
                .Include(p => p.WrittenMovies)
                .Where(p => names.Select(n => n.Trim()).Contains(p.Name));
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
