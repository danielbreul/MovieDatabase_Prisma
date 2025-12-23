using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Dtos;

namespace MovieDatabaseBackend.Controllers
{
    [Route("directors")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        [HttpGet("{id:int}/movies")]
        public ActionResult<IEnumerable<MovieDto>> Get(int id)
        {
            return Ok(new MovieDto(5, ""));
        }
    }
}
