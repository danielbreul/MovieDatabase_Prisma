using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Services;

namespace MovieDatabaseBackend.Controllers
{
    [Route("genres")]
    [ApiController]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService _service = genreService;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<GenreDto>> Get()
        {
            var result = _service.GetGenres();
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
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<GenreDto> Post(GenreCreateUpdateDto genreCreateDto)
        {
            var result = _service.CreateGenre(genreCreateDto);
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
        public IActionResult Put(int id, [FromBody] GenreCreateUpdateDto genreUpdateDto)
        {
            var result = _service.UpdateGenre(id, genreUpdateDto);
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
        [ProducesResponseType(StatusCodes.Status409Conflict, Description = "Cannot delete referenced genre.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var result = _service.DeleteGenre(id);
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

        [HttpGet("{id:int}/movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<MovieDto>> GetMovies(int id)
        {
            var result = _service.GetMovies(id);
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
