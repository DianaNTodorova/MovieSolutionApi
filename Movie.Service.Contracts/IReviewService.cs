using Movie.Core.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto?> GetReviewByIdAsync(int id);
        Task<bool> ReviewExistsAsync(int id);
        Task AddReviewAsync(ReviewDto reviewDto);
        Task UpdateReviewAsync(ReviewDto reviewDto);
        Task DeleteReviewAsync(int id);
    }
}
