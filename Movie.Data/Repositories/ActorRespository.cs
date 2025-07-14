using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.Repositories
{
    public class ActorRespository : IActorRepository
    {
        public void Add(Actor actor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Actor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Actor> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Actor actor)
        {
            throw new NotImplementedException();
        }

        public void Update(Actor actor)
        {
            throw new NotImplementedException();
        }
    }
}
