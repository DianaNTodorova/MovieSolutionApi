using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Core.Domain.Contracts;
using MovieApi.Data;

namespace MovieApi.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public MoviesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return Ok(movieDtos);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _unitOfWork.Movies.GetAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }
        //THIS NEEDS TO BE CHANGED
        // GET: api/Movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id)
        {
            var movie = await _unitOfWork.Movies.GetMovieDetailsDtoAsync(id);


            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }


        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var movie = await _unitOfWork.Movies.GetAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _mapper.Map(movieDto, movie);
            _unitOfWork.Movies.Update(movie);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Optional: Check if the movie still exists (in case of concurrency issues)
                if (!await _unitOfWork.Movies.AnyAsync(id))
                {
                    return NotFound();
                }

                throw; // Let the exception bubble up if it's another issue
            }

            return NoContent();
        }

        //this need to be reviewd
        [HttpPost("{movieId}/actors")]
        public async Task<IActionResult> AddActorToMovie(int movieId, MovieActorCreateDto dto)
        {
            var movie = await _unitOfWork.Movies.AnyAsync(movieId);
            if (!movie) return NotFound();

            var actor = await _unitOfWork.Actors.AnyAsync(dto.ActorId);
            if (!actor) return NotFound();

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = dto.ActorId,
                Role = dto.Role
            };

            _unitOfWork.MovieActors.Add(movieActor);
            await _unitOfWork.CompleteAsync();

            return Ok(movieActor);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movieDto)
        {
            var movie = _mapper.Map<Movies>(movieDto);

            _unitOfWork.Movies.Add(movie);
            await _unitOfWork.CompleteAsync();

            var createdMovieDto = _mapper.Map<MovieDto>(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = createdMovieDto.Id }, createdMovieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _unitOfWork.Movies
                .GetAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var deletedMovieDto = _mapper.Map<MovieDto>(movie);

            _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.CompleteAsync();

            return Ok(deletedMovieDto);
        }

        private async Task<bool> MovieExists(int id)
        {
            return await _unitOfWork.Movies.AnyAsync(id);
        }
    }

}
