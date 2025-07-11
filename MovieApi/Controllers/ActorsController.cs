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
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieApiContext _context;
        private readonly IMapper _mapper;

        public ActorsController(MovieApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActor()
        {
            var actors = await _context.Actor.ToListAsync();
            var actorDtos = _mapper.Map<IEnumerable<ActorDto>>(actors);
            return Ok(actorDtos);
        }

        // GET: api/actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _context.Actor.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            var actorDto = _mapper.Map<ActorDto>(actor);
            return Ok(actorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorDto actorDto)
        {
            if (id != actorDto.Id)
            {
                return BadRequest();
            }

           
            var actor = await _context.Actor
                .Include(a => a.Movie) 
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

           
            _mapper.Map(actorDto, actor);

           
            if (actorDto.MovieIds != null)
            {
                // Actor => Movies based on the list of Movie IDs in actorDto
                var movies = await _context.Movie
                    .Where(m => actorDto.MovieIds.Contains(m.Id))
                    .ToListAsync();

                actor.Movie = movies;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/actors
        [HttpPost]
        public async Task<ActionResult<ActorDto>> PostActor(ActorDto actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            _context.Actor.Add(actor);
            await _context.SaveChangesAsync();

            var createdActorDto = _mapper.Map<ActorDto>(actor);
            return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, createdActorDto);
        }

        // DELETE: api/actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return _context.Actor.Any(e => e.Id == id);
        }
    }
}
