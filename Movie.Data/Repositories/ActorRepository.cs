using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.Entities;
using MovieApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly MovieDbContext _context;
        public ActorRepository(MovieDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Actor.AnyAsync(m=>m.Id==id);
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _context.Actor.ToListAsync(); 
        }

        public async Task<Actor> GetAsync(int id)
        {
            return await _context.Actor.FirstOrDefaultAsync(m => m.Id == id);
        }
        public void Add(Actor actor)
        {
            _context.Actor.Add(actor);
        }

        public void Remove(Actor actor)
        {
            _context.Actor.Remove(actor);
        }

        public void Update(Actor actor)
        {
            _context.Actor.Update(actor);
        }
    }
}
