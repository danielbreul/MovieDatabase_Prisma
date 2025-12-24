using MovieDatabaseBackend.Dtos;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Services;

namespace MovieDatabaseBackend.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MoviesController(IMovieService movieService) : ControllerBase
    {
        private readonly IMovieService _service = movieService;

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> Get()
        {
            var movies = _service.GetMovies();
            if (movies.Any())
            {
                return Ok(movies);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<MovieDetailDto> Get(int id)
        {
            var movie = _service.GetMovie(id);
            if (movie is not null)
            {
                return Ok(movie);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] MovieCreateUpdateDto movieCreateDto)
        {
            var createdMovieDto = _service.CreateMovie(movieCreateDto);
            if (createdMovieDto.Success)
            {
                return Created("/movies/" + createdMovieDto.Value.Id, createdMovieDto.Value);
            }
            else
            {
                return BadRequest(createdMovieDto.Error);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] MovieCreateUpdateDto movieUpdateDto)
        {
            if (_service.UpdateMovie(id, movieUpdateDto))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (_service.DeleteMovie(id))
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
