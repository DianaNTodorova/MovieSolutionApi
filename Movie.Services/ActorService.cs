using AutoMapper;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using Movie.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> ActorExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddActorAsync(ActorDto actorDto)
        {
            var exists = await _unitOfWork.Actors.AnyAsync(actorDto.Id);
            if (exists)
                throw new InvalidOperationException("Actor already exists.");


            var actor = _mapper.Map<Actor>(actorDto);
            _unitOfWork.Actors.Add(actor);
            await _unitOfWork.SaveAsync();

        }
        public async Task AssignActorToMovieAsync(int actorId, int movieId, string role)
        {
            var actor = await _unitOfWork.Actors.GetAsync(actorId);
            if (actor == null)
                throw new InvalidOperationException("Actor not found.");

            var movie = await _unitOfWork.Movies.GetMovieWithActorsAsync(movieId);
            if (movie == null)
                throw new InvalidOperationException("Movie not found.");

            var alreadyAssigned = await _unitOfWork.MovieActors.AnyAsync(movieId, actorId);
            if (alreadyAssigned)
                throw new InvalidOperationException("Skådespelaren är redan tilldelad denna film.");

            var movieActor = new MovieActor
            {
                ActorId = actorId,
                MovieId = movieId,
                Role = role
            };

            _unitOfWork.MovieActors.Add(movieActor);
            await _unitOfWork.SaveAsync();
        }


        public async Task DeleteActorAsync(int id)
        {
            var actor = await _unitOfWork.Actors.GetAsync(id);
            if (actor == null)
                throw new InvalidOperationException("Actor not found.");
            _unitOfWork.Actors.Remove(actor);
            await _unitOfWork.SaveAsync();

        }

        public async Task<ActorDto?> GetActorByIdAsync(int id)
        {
            var actor = await _unitOfWork.Actors.GetAsync(id);
            if (actor == null)
                return null;
            return _mapper.Map<ActorDto>(actor);

        }

        public async Task<IEnumerable<ActorDto>> GetAllActorsAsync()
        {
            var actors = await _unitOfWork.Actors.GetAllAsync();
            return _mapper.Map<IEnumerable<ActorDto>>(actors);

        }

        public async Task UpdateActorAsync(ActorDto actorDto)
        {
            var actor = await _unitOfWork.Actors.GetAsync(actorDto.Id);
            if (actor == null)
                throw new InvalidOperationException("Actor not found.");
            _mapper.Map(actorDto, actor);
            _unitOfWork.Actors.Update(actor);
            await _unitOfWork.SaveAsync();

        }
    }
}
