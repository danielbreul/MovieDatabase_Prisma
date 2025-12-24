using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Services;

namespace MovieDatabaseBackend.Controllers
{
    [Route("writers")]
    [ApiController]
    public class WritersController(IPersonService personService) : ControllerBase
    {
        private readonly IPersonService _service = personService;

        [HttpGet("{id:int}/movies")]
        public ActionResult<IEnumerable<MovieDto>> Get(int id)
        {
            var movies = _service.GetWrittenMovies(id);
            if (movies.Any())
            {
                return Ok(movies);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
