using Microsoft.AspNetCore.Mvc;
using MovieDatabaseBackend.Common;
using MovieDatabaseBackend.Dtos;
using MovieDatabaseBackend.Services;
using System.ComponentModel;

namespace MovieDatabaseBackend.Controllers
{
    [Route("genres")]
    [ApiController]
    public class GenresController(IGenreService genreService) : ControllerBase
    {
        private readonly IGenreService _service = genreService;

        /// <summary>
        ///     Retrieves all genres.
        /// </summary>
        /// <response code="200">List of all genres</response>
        /// <response code="204">No genres in store</response>
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

        /// <summary>
        ///     Creates a new genre.
        /// </summary>
        /// <response code="201">Genre successfully created</response>
        /// <response code="400">Invalid genre data provided</response>
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

        /// <summary>
        ///     Updates an existing genre.
        /// </summary>
        /// <response code="200">Genre successfully updated</response>
        /// <response code="400">Invalid genre data provided</response>
        /// <response code="404">Genre not found</response>
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

        /// <summary>
        ///     Deletes an existing genre.
        /// </summary>
        /// <response code="204">Genre successfully deleted</response>
        /// <response code="404">Genre not found</response>
        /// <response code="409">Cannot delete genre referenced by movies</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

        /// <summary>
        ///     Retrieves all movies for a specific genre.
        /// </summary>
        /// <response code="200">List of movies in the genre</response>
        /// <response code="204">No movies found for the genre</response>
        /// <response code="404">Genre not found</response>
        [HttpGet("{id:int}/movies")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MovieDto>> GetMovies(int id)
        {
            var result = _service.GetMovies(id);
            if (result.IsSucceed)
            {
                if (result.Value.Any())
                {
                    return Ok(result.Value);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
