using Movie.Core.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Domain.Contracts
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review>? GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Review review);
        void Update(Review review);
        void Remove(Review review);
        Task<IEnumerable<Review>> GetAllByMovieIdAsync(int movieId);
    }
}
