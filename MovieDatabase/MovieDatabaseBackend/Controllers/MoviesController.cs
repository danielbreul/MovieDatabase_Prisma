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

        /// <summary>
        ///     Retrieves all movies.
        /// </summary>
        /// <response code="200">List of all movies</response>
        /// <response code="204">No movies in store</response>
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

        /// <summary>
        ///     Searches for movies by title.
        /// </summary>
        /// <param name="title">Optional title to filter movies</param>
        /// <response code="200">List of matching movies</response>
        /// <response code="204">No matching movies found</response>
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

        /// <summary>
        ///     Retrieves movie details by ID.
        /// </summary>
        /// <response code="200">Movie details</response>
        /// <response code="404">Movie not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MovieDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        ///     Creates a new movie.
        /// </summary>
        /// <response code="201">Movie successfully created</response>
        /// <response code="400">Invalid movie data provided</response>
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

        /// <summary>
        ///     Updates an existing movie.
        /// </summary>
        /// <response code="200">Movie successfully updated</response>
        /// <response code="400">Invalid movie data provided</response>
        /// <response code="404">Movie not found</response>
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

        /// <summary>
        ///     Deletes an existing movie.
        /// </summary>
        /// <response code="204">Movie successfully deleted</response>
        /// <response code="404">Movie not found</response>
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
