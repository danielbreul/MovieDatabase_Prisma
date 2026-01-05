using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Services;

namespace MovieDatabaseBackend.Controllers
{
    [Route("persons")]
    [ApiController]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        private readonly IPersonService _service = personService;

        /// <summary>
        ///     Retrieves all persons.
        /// </summary>
        /// <response code="200">List of all persons</response>
        /// <response code="204">No persons in store</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<PersonDto>> Get()
        {
            var result = _service.GetPersons();
            if (result.IsSucceed)
            {
                return Ok(result.Value);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        ///     Creates a new person.
        /// </summary>
        /// <response code="201">Person successfully created</response>
        /// <response code="400">Invalid person data provided</response>
        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDto> Post([FromBody] PersonCreateUpdateDto personCreateDto)
        {
            var result = _service.CreatePerson(personCreateDto);
            if (result.IsSucceed)
            {
                return StatusCode(StatusCodes.Status201Created, result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        /// <summary>
        ///     Updates an existing person.
        /// </summary>
        /// <response code="200">Person successfully updated</response>
        /// <response code="400">Invalid person data provided</response>
        /// <response code="404">Person not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] PersonCreateUpdateDto personUpdateDto)
        {
            var result = _service.UpdatePerson(id, personUpdateDto);
            if (result.IsSucceed)
            {
                return Ok();
            }
            else if (result.State == ResultState.InvalidDto)
            {
                return BadRequest(result.Error);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Deletes an existing person.
        /// </summary>
        /// <response code="204">Person successfully deleted</response>
        /// <response code="404">Person not found</response>
        /// <response code="409">Cannot delete person referenced by movies</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Delete(int id)
        {
            var result = _service.DeletePerson(id);
            if (result.IsSucceed)
            {
                return NoContent();
            }
            else if (result.State == ResultState.Referenced)
            {
                return Conflict(result.Error);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Retrieves all movies directed by a specific person.
        /// </summary>
        /// <response code="200">List of movies directed by the person</response>
        /// <response code="204">No movies found for the person</response>
        /// <response code="404">Person not found</response>
        [HttpGet("{id:int}/directed-movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MovieDto>> GetDirectedMovies(int id)
        {
            var result = _service.GetDirectedMovies(id);
            if (result.IsSucceed)
            {
                if (result.Value.Any())
                {
                    return Ok(result.Value);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        ///     Retrieves all movies written by a specific person.
        /// </summary>
        /// <response code="200">List of movies written by the person</response>
        /// <response code="204">No movies found for the person</response>
        /// <response code="404">Person not found</response>
        [HttpGet("{id:int}/written-movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MovieDto>> GetWrittenMovies(int id)
        {
            var result = _service.GetWrittenMovies(id);
            if (result.IsSucceed)
            {
                if (result.Value.Any())
                {
                    return Ok(result.Value);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        ///     Retrieves all movies a specific person acted in.
        /// </summary>
        /// <response code="200">List of movies the person acted in</response>
        /// <response code="204">No movies found for the person</response>
        /// <response code="404">Person not found</response>
        [HttpGet("{id:int}/acted-in-movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MovieDto>> GetActedInMovies(int id)
        {
            var result = _service.GetActedInMovies(id);
            if (result.IsSucceed)
            {
                if (result.Value.Any())
                {
                    return Ok(result.Value);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NoContent();
            }
        }
    }
}
