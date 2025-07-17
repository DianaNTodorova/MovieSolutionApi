using Movie.Core.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Domain.Contracts
{
    public interface IMovieActorRepository
    {
        Task<IEnumerable<MovieActor>> GetByActorIdAsync(int actorId);

        void Add(MovieActor movieActor);
        void Remove(MovieActor movieActor);
        Task<List<MovieActor>> GetAllAsync();
        Task<bool> AnyAsync(int movieId, int actorId);
        Task AnyAsync(MovieActor movieActor);
    }
}
