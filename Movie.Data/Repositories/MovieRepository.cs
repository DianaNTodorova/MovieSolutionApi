using Microsoft.EntityFrameworkCore;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using MovieApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;
        public MovieRepository(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movies>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.MovieActors)
                .Include(m => m.Genres)
                .ToListAsync();
        }

        public async Task<MovieDetailsDto?> GetMovieDetailsDtoAsync(int id)
        {
            return await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailsDto
                {
                    Synopsis = m.MovieDetails.Synopsis,
                    Language = m.MovieDetails.Language,
                    Budget = m.MovieDetails.Budget,
                    
                    Actors = m.MovieActors.Select(a => new ActorDto
                    {
                        Id = a.Actor.Id,
                        Name = a.Actor.Name,
                        BirthYear = a.Actor.BirthYear
                    }).ToList(),
                    Reviews = m.Reviews.Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Comment = r.Comment,
                        Rating = r.Rating,
                        MovieIds = r.MovieId
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Movies?> GetAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.MovieActors)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Add(Movies movie)
        {
            _context.Movies.Add(movie);
        }

        public void Remove(Movies movie)
        {
            _context.Movies.Remove(movie);
        }

        public void Update(Movies movie)
        {
            _context.Movies.Update(movie);
        }

        public async Task AddAsync(Movies movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
        public async Task<Movies> GetMovieWithActorsAsync(int movieId)
        {
            return await _context.Movies
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == movieId);
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found.");
            }
        }

    }
}
