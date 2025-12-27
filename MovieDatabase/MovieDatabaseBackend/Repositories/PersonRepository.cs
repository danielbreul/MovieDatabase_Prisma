using Microsoft.EntityFrameworkCore;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Repositories
{
    public class PersonRepository(MovieDbContext context) : IPersonRepository
    {
        private readonly MovieDbContext _context = context;

        public IQueryable<int> FilterExistingPersonIds(IEnumerable<int> ids)
        {
            return _context.Persons
                .Where(g => ids.Contains(g.Id))
                .Select(g => g.Id);
        }

        public IQueryable<Person> GetPersonsByIds(IEnumerable<int> ids)
        {
            return _context.Persons
                .Where(g => ids.Contains(g.Id));
        }

        public IQueryable<Person> GetPersons()
        {
            return _context.Persons
                .Include(p => p.DirectedMovies)
                .Include(p => p.ActedInMovies)
                .Include(p => p.WrittenMovies);
        }

        public Person? GetPerson(int id)
        {
            return _context.Persons
                .Include(p => p.DirectedMovies)
                .Include(p => p.ActedInMovies)
                .Include(p => p.WrittenMovies)
                .FirstOrDefault(p => p.Id == id);
        }

        public Person AddPerson(Person person)
        {
            return _context.Persons.Add(person).Entity;
        }

        public void RemovePerson(Person person)
        {
            _context.Persons.Remove(person);
        }
    }
}
