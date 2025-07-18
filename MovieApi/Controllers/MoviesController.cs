using Microsoft.AspNetCore.Mvc;
using Movie.Core.Domain.Contracts.Utilities;
using Movie.Core.Domain.Models.DTOs;
using Movie.Service.Contracts;
using System.Text.Json;

namespace MovieApi.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public MoviesController(IServiceManager service)
        {
            _service = service;
        }
        private static readonly List<MovieDto> _movies = Enumerable.Range(1, 250).Select(i =>
           new MovieDto { Id = i, Title = $"Movie {i}" }
       ).ToList();

        [HttpGet("pages")]
        public ActionResult<PagedResult<MovieDto>> GetMoviesPaging([FromQuery] PagingParameters pagingParams)
        {
            var totalItems = _movies.Count;

            var items = _movies
                .Skip((pagingParams.Page - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToList();

            var result = new PagedResult<MovieDto>(items, totalItems, pagingParams.Page, pagingParams.PageSize);

            var paginationMetadata = new
            {
                totalItems = result.TotalItems,
                currentPage = result.CurrentPage,
                totalPages = result.TotalPages,
                pageSize = result.PageSize
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(result);
        }
        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _service.MovieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        // GET: api/Movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _service.MovieService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // GET: api/Movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id)
        {
            var details = await _service.MovieService.GetMovieDetailsAsync(id);
            if (details == null)
                return NotFound();

            return Ok(details);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto dto)
        {
            var createdMovie = await _service.MovieService.CreateMovieAsync(dto);
            await _service.SaveAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
        }

        // PUT: api/Movies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Movie ID mismatch.");

            var updated = await _service.MovieService.UpdateMovieAsync(id, dto);
            if (!updated)
                return NotFound();

            await _service.SaveAsync();
            return NoContent();
        }

        // DELETE: api/Movies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deletedMovie = await _service.MovieService.DeleteMovieAsync(id);
            if (deletedMovie == null)
                return NotFound();

            await _service.SaveAsync();
            return Ok(deletedMovie);
        }

        // POST: api/Movies/{movieId}/actors
        [HttpPost("{movieId}/actors")]
        public async Task<IActionResult> AddActorToMovie(int movieId, MovieActorCreateDto dto)
        {
            try
            {
                await _service.MovieService.AddActorToMovieAsync(movieId, dto);
                await _service.SaveAsync();
                return Ok(new { Message = "Actor added to movie." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
