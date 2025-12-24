using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Services;

namespace MovieDatabaseBackend.Controllers
{
    [Route("genres")]
    [ApiController]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService _service = genreService;

        [HttpGet("{id:int}/movies")]
        public ActionResult<IEnumerable<MovieDto>> Get(int id)
        {
            var movies = _service.GetMovies(id);
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
