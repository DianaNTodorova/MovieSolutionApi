using AutoMapper;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using Movie.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddReviewAsync(ReviewDto reviewDto)
        {
         var exists = await _unitOfWork.Reviews.AnyAsync(reviewDto.Id);
            if (exists)
                throw new InvalidOperationException("Review already exists.");

            var movie = await _unitOfWork.Movies.GetAsync(reviewDto.MovieIds);
            if (movie.Reviews.Count >= 10)
                throw new InvalidOperationException("A movie can have a maximum of 10 reviews.");
            if (movie.Year != DateTime.MinValue)

            {
                int age = DateTime.UtcNow.Year - movie.Year.Year;
                if (age >= 20)
                {
                    await CleanupOldMovieReviewsForMovieAsync(movie.Id);
                }
            }

            var review = _mapper.Map<Review>(reviewDto);
            _unitOfWork.Reviews.Add(review);
            await _unitOfWork.SaveAsync();
        }

        public async Task CleanupOldMovieReviewsForMovieAsync(int movieId)
        {
            var allReviews = await _unitOfWork.Reviews.GetAllAsync();

            var reviews = allReviews
                .Where(r => r.MovieId == movieId)
                .OrderBy(r => r.Id)  // Or use CreatedAt if available
                .ToList();

            if (reviews.Count > 5)
            {
                int reviewsToRemove = reviews.Count - 5;
                for (int i = 0; i < reviewsToRemove; i++)
                {
                    _unitOfWork.Reviews.Remove(reviews[i]);
                }
                await _unitOfWork.SaveAsync();
            }
        }





        public async Task DeleteReviewAsync(int id)
        {
          var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null)
                throw new InvalidOperationException("Review not found.");
            _unitOfWork.Reviews.Remove(review);
            await _unitOfWork.SaveAsync();

        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _unitOfWork.Reviews.GetAllAsync();
            return reviews.Select(review => _mapper.Map<ReviewDto>(review)).ToList();


        }

        public async Task<ReviewDto?> GetReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null)
                return null;
            return _mapper.Map<ReviewDto>(review);

        }

        public async Task<bool> ReviewExistsAsync(int id)
        {
            var exists = await _unitOfWork.Reviews.AnyAsync(id);
            return exists;
        }

        public async Task<bool> UpdateReviewAsync(ReviewDto reviewDto)
        {
            var exists = await _unitOfWork.Reviews.AnyAsync(reviewDto.Id);
            if (!exists)
                throw new InvalidOperationException("Review does not exist.");
            var review = _mapper.Map<Review>(reviewDto);
            _unitOfWork.Reviews.Update(review);
            await _unitOfWork.SaveAsync();
            return exists;
    
        }

        Task IReviewService.UpdateReviewAsync(ReviewDto reviewDto)
        {
            return UpdateReviewAsync(reviewDto);
        }
    }
}
