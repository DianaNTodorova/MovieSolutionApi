using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models;
using MovieApi.Models.DTOs;

namespace MovieApi.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieApiContext _context;
        private readonly IMapper _mapper;

        public MoviesController(MovieApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
        {
            var movies = await _context.Movie
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.Actor)
                .ToListAsync();

            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return Ok(movieDtos);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _context.Movie
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }

        // GET: api/Movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id)
        {
            var movie = await _context.Movie
                .Where(m => m.Id == id)
                .Select(m => new //using anonymous function worked with nested details
                {
                    Details= new MovieDetailsDto
                    {
                        Synopsis = m.MovieDetails.Synopsis,
                        Language = m.MovieDetails.Language,
                        Budget = m.MovieDetails.Budget,
                    },
                    Actor = m.Actor.Select(a => new ActorDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        BirthYear = a.BirthYear
                    }).ToList(),
                    Review = m.Reviews.Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Comment = r.Comment,
                        Rating = r.Rating,
                        MovieIds = new List<int> { r.MovieId }
                    }).ToList()
                })
                .FirstOrDefaultAsync();

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

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _mapper.Map(movieDto, movie);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MovieExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost("{movieId}/actors")]
        public async Task<IActionResult> AddActorToMovie(int movieId, MovieActorCreateDto dto)
        {
            var movie = await _context.Movie.FindAsync(movieId);
            if (movie == null) return NotFound();

            var actor = await _context.Actor.FindAsync(dto.ActorId);
            if (actor == null) return NotFound();

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = dto.ActorId,
                Role = dto.Role
            };

            _context.MovieActor.Add(movieActor);
            await _context.SaveChangesAsync();

            return Ok(movieActor);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);

            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            var createdMovieDto = _mapper.Map<MovieDto>(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = createdMovieDto.Id }, createdMovieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movie
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var deletedMovieDto = _mapper.Map<MovieDto>(movie);

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(deletedMovieDto);
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }

}
