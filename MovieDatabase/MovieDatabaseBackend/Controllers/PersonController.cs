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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Description = "Cannot delete referenced person.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpGet("{id:int}/directed-movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<MovieDto>> GetDirectedMovies(int id)
        {
            var result = _service.GetDirectedMovies(id);
            if (result.IsSucceed)
            {
                return Ok(result.Value);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id:int}/written-movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<MovieDto>> GetWrittenMovies(int id)
        {
            var result = _service.GetWrittenMovies(id);
            if (result.IsSucceed)
            {
                return Ok(result.Value);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id:int}/acted-in-movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<MovieDto>> GetActedInMovies(int id)
        {
            var result = _service.GetActedInMovies(id);
            if (result.IsSucceed)
            {
                return Ok(result.Value);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
