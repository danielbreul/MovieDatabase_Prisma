using Microsoft.AspNetCore.Http;
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
            return Ok(_service.GetMovies());
        }

        [HttpGet("{id:int}")]
        public ActionResult<MovieDetailDto> Get(int id)
        {
            var movie = _service.GetMovie(id);
            if (movie != null)
            {
                return Ok(movie);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:int}")]
        public IActionResult Post(int id, [FromBody] MovieCreateUpdateDto movieCreateDto)
        {
            _service.CreateMovie(movieCreateDto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] MovieCreateUpdateDto movieUpdateDto)
        {
            _service.UpdateMovie(id, movieUpdateDto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _service.DeleteMovie(id);
            return NoContent();
        }
    }
}
