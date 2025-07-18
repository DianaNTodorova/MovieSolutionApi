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
    public class ReviewRepository : IReviewRepository
    {
        private readonly MovieDbContext _context;
        public ReviewRepository(MovieDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Reviews.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(m => m.Id == id);
        }
        public void Add(Review review)
        {
            _context.Reviews.Add(review);
        }

        public void Remove(Review review)
        {
            _context.Reviews.Remove(review);
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
        }

        public async Task<IEnumerable<Review>> GetAllByMovieIdAsync(int movieId)
        {
            return await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .ToListAsync();
        }
    }
}
