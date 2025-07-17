using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Core.Domain.Models.DTOs;
using Movie.Service.Contracts;  // for IServiceManager
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ActorsController(IServiceManager service)
        {
            _service = service;
        }

        // GET: api/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _service.ActorService.GetAllActorsAsync();
            return Ok(actors);
        }

        // GET: api/actors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _service.ActorService.GetActorByIdAsync(id);
            if (actor == null)
                return NotFound();
            return Ok(actor);
        }

        // PUT: api/actors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorDto actorDto)
        {
            if (id != actorDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                await _service.ActorService.UpdateActorAsync(actorDto);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/actors
        [HttpPost]
        public async Task<ActionResult<ActorDto>> PostActor(ActorDto actorDto)
        {
            try
            {
                await _service.ActorService.AddActorAsync(actorDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

            return CreatedAtAction(nameof(GetActor), new { id = actorDto.Id }, actorDto);
        }

        // DELETE: api/actors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            try
            {
                await _service.ActorService.DeleteActorAsync(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
