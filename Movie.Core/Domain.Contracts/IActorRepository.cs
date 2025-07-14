using Movie.Core.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Domain.Contracts
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Actor actor);
        void Update(Actor actor);
        void Remove(Actor actor);
    }
}
