using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Services;

namespace MovieDatabaseBackend.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MoviesController(IMovieService movieService) : ControllerBase
    {
        private readonly IMovieService _service = movieService;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<MovieDto>> Get()
        {
            var result = _service.GetMovies();
            if (result.IsSucceed)
            {
                return Ok(result.Value);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<MovieDto>> Search([FromQuery] string? title)
        {
            var result = _service.GetMovies(title);
            if (result.IsSucceed)
            {
                return Ok(result.Value);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MovieDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<MovieDetailDto> Get(int id)
        {
            var result = _service.GetMovie(id);
            if (result.IsSucceed)
            {
                return Ok(result.Value);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(MovieDetailDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MovieDetailDto> Post([FromBody] MovieCreateUpdateDto movieCreateDto)
        {
            var result = _service.CreateMovie(movieCreateDto);
            if (result.IsSucceed)
            {
                return Created("/movies/" + result.Value.Id, result.Value);
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
        public IActionResult Put(int id, [FromBody] MovieCreateUpdateDto movieUpdateDto)
        {
            var result = _service.UpdateMovie(id, movieUpdateDto);
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (_service.DeleteMovie(id).IsSucceed)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
