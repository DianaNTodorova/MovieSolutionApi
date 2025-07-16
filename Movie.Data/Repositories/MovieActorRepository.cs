using Microsoft.EntityFrameworkCore;
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
    public class MovieActorRepository : IMovieActorRepository
    {
        private readonly MovieDbContext _context;
        public MovieActorRepository(MovieDbContext context)
        {
            _context = context;
        }
        public void Add(MovieActor movieActor)
        {
            _context.MovieActor.Add(movieActor);
        }
        public void Remove(MovieActor movieActor)
        {
            _context.MovieActor.Remove(movieActor);
        }

        public async Task<List<MovieActor>> GetAllAsync()
        {
            return await _context.MovieActor
                .Include(ma => ma.Actor)
                .Include(ma => ma.Movie)
                .ToListAsync();
        }
        public async Task<IEnumerable<MovieActor>> GetByActorIdAsync(int actorId)
        {
            return await _context.MovieActor
                .Where(ma => ma.ActorId == actorId)
                .Include(ma => ma.Movie)
                .ToListAsync();
        }
        public async Task<bool> AnyAsync(int movieId, int actorId)
        {
            return await _context.MovieActor
                .AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);
        }


    }
}
