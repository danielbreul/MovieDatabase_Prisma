using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Data;

namespace MovieDatabaseBackend.Services
{
    public class PersonService(IUnitOfWork unitOfWork) : IPersonService
    {
        private readonly IUnitOfWork _uof = unitOfWork;

        public IEnumerable<MovieDto> GetDirectedMovies(int writerId)
        {
            var person = _uof.Persons.GetPersonById(writerId);
            if (person is null)
            {
                return [];
            }
            else
            {
                return person.DirectedMovies.Select(m => new MovieDto(m.Id, m.Title));
            }
        }

        public IEnumerable<MovieDto> GetActedInMovies(int writerId)
        {
            var person = _uof.Persons.GetPersonById(writerId);
            if (person is null)
            {
                return [];
            }
            else
            {
                return person.ActedInMovies.Select(m => new MovieDto(m.Id, m.Title));
            }
        }

        public IEnumerable<MovieDto> GetWrittenMovies(int writerId)
        {
            var person = _uof.Persons.GetPersonById(writerId);
            if (person is null)
            {
                return [];
            }
            else
            {
                return person.WrittenMovies.Select(m => new MovieDto(m.Id, m.Title));
            }
        }
    }
}
