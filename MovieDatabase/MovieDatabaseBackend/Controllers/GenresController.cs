using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Controllers
{

    [Route("genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        [HttpGet("{id:int}/movies")]
        public ActionResult<IEnumerable<MovieDto>> Get(int id)
        {
            return Ok(new MovieDto(5, ""));
        }
    }
}
