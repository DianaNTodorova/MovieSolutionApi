using Movie.Core.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Contracts
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDto>> GetAllActorsAsync();
        Task<ActorDto?> GetActorByIdAsync(int id);
        Task<bool> ActorExistsAsync(int id);
        Task AddActorAsync(ActorDto actorDto);
        Task UpdateActorAsync(ActorDto actorDto);
        Task DeleteActorAsync(int id);
    }
}
