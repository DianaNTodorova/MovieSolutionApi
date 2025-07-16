using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _unitOfWork.Actors.GetAllAsync();
            var actorDtos = _mapper.Map<IEnumerable<ActorDto>>(actors);
            return Ok(actorDtos);
        }

        // GET: api/actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _unitOfWork.Actors.GetAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            var actorDto = _mapper.Map<ActorDto>(actor);
            return Ok(actorDto);
        }

        // PUT: api/actors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorDto actorDto)
        {
            if (id != actorDto.Id)
                return BadRequest("ID mismatch");

            var actor = await _unitOfWork.Actors.GetAsync(id);
            if (actor == null)
                return NotFound();

            _mapper.Map(actorDto, actor);
            _unitOfWork.Actors.Update(actor);

           
            if (actorDto.MovieIds != null)
            {
               
                var existingLinks = await _unitOfWork.MovieActors.GetByActorIdAsync(id);
                foreach (var link in existingLinks)
                {
                    _unitOfWork.MovieActors.Remove(link);
                }

                
                foreach (var movieId in actorDto.MovieIds)
                {
                    _unitOfWork.MovieActors.Add(new MovieActor
                    {
                        ActorId = id,
                        MovieId = movieId,
                        Role = "Unknown" 
                    });
                }
            }

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        // POST: api/actors
        [HttpPost]
        public async Task<ActionResult<ActorDto>> PostActor(ActorDto actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            _unitOfWork.Actors.Add(actor);
            await _unitOfWork.CompleteAsync();

            var createdDto = _mapper.Map<ActorDto>(actor);
            return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, createdDto);
        }

        // DELETE: api/actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _unitOfWork.Actors.GetAsync(id);
            if (actor == null)
                return NotFound();


            _unitOfWork.Actors.Remove(actor);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // Utility
        private async Task<bool> ActorExists(int id)
        {
            return await _unitOfWork.Actors.AnyAsync(id);
        }
    }
}
