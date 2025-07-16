using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using MovieApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Include(m => m.Actor)
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
                        Budget = m.MovieDetails.Budget
                   ,
                    Actors = m.Actor.Select(a => new ActorDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        BirthYear = a.BirthYear
                    }).ToList(),
                    Reviews = m.Reviews.Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Comment = r.Comment,
                        Rating = r.Rating,
                        MovieIds =  r.MovieId
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }


        public async Task<Movies?> GetAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.Actor)
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
    }
}
