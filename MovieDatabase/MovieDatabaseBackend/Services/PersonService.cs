using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Data;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Entities;

namespace MovieDatabaseBackend.Services
{
    public class PersonService(IUnitOfWork unitOfWork) : IPersonService
    {
        private readonly IUnitOfWork _uof = unitOfWork;

        public Result<IEnumerable<PersonDto>> GetPersons()
        {
            var persons = _uof.Persons.GetPersons();
            if (persons is null)
            {
                return Result<IEnumerable<PersonDto>>.Fail(ResultState.NotFound);
            }
            else
            {
                return Result<IEnumerable<PersonDto>>.Ok(persons.Select(p => new PersonDto { Id = p.Id, Name = p.Name }));
            }
        }

        public Result<PersonDto?> CreatePerson(PersonCreateUpdateDto personDto)
        {
            // Name is required
            if (string.IsNullOrWhiteSpace(personDto.Name))
            {
                return Result<PersonDto?>.Fail(ResultState.InvalidDto, "Name is required!");
            }

            var person = new Person
            {
                Name = personDto.Name
            };

            _uof.Persons.AddPerson(person);
            _uof.SaveChanges();

            return Result<PersonDto?>.Ok(new PersonDto { Id = person.Id, Name = person.Name });
        }

        public Result UpdatePerson(int id, PersonCreateUpdateDto personDto)
        {
            // Name is required
            if (string.IsNullOrWhiteSpace(personDto.Name))
            {
                return Result.Fail(ResultState.InvalidDto, "Name is required!");
            }

            var person = _uof.Persons.GetPerson(id);
            if (person is null)
            {
                return Result.Fail(ResultState.NotFound);
            }

            person.Name = personDto.Name;

            _uof.SaveChanges();
            return Result.Ok();
        }

        public Result DeletePerson(int id)
        {
            var person = _uof.Persons.GetPerson(id);
            if (person is null)
            {
                return Result.Fail(ResultState.NotFound);
            }
            else if (person.DirectedMovies.Count != 0 || person.WrittenMovies.Count != 0 || person.ActedInMovies.Count != 0)
            {
                return Result.Fail(ResultState.Referenced, "Cannot delete referenced person!");
            }

            _uof.Persons.RemovePerson(person);
            _uof.SaveChanges();

            return Result.Ok();
        }

        public Result<IEnumerable<MovieDto>> GetDirectedMovies(int personId)
        {
            var person = _uof.Persons.GetPerson(personId);
            if (person is not null)
            {
                var movies = person.DirectedMovies.Select(m => new MovieDto { Id = m.Id, Title = m.Title });
                if (movies.Any())
                {
                    return Result<IEnumerable<MovieDto>>.Ok(movies);
                }
            }
            return Result<IEnumerable<MovieDto>>.Fail(ResultState.NotFound);
        }

        public Result<IEnumerable<MovieDto>> GetActedInMovies(int personId)
        {
            var person = _uof.Persons.GetPerson(personId);
            if (person is not null)
            {
                var movies = person.ActedInMovies.Select(m => new MovieDto { Id = m.Id, Title = m.Title });
                if (movies.Any())
                {
                    return Result<IEnumerable<MovieDto>>.Ok(movies);
                }
            }
            return Result<IEnumerable<MovieDto>>.Fail(ResultState.NotFound);
        }

        public Result<IEnumerable<MovieDto>> GetWrittenMovies(int personId)
        {
            var person = _uof.Persons.GetPerson(personId);
            if (person is not null)
            {
                var movies = person.WrittenMovies.Select(m => new MovieDto { Id = m.Id, Title = m.Title });
                if (movies.Any())
                {
                    return Result<IEnumerable<MovieDto>>.Ok(movies);
                }
            }
            return Result<IEnumerable<MovieDto>>.Fail(ResultState.NotFound);
        }
    }
}
