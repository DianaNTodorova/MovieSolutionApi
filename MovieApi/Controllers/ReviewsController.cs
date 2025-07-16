using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = await _unitOfWork.Reviews.GetAllAsync();
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        // GET: api/reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null) return NotFound();

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return Ok(reviewDto);
        }

        // PUT: api/reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id)
                return BadRequest("ID mismatch");

            var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null)
                return NotFound();

            _mapper.Map(reviewDto, review);

            // Optional: Handle related movies if needed
            // If Review has a Movie relationship (one-to-many):
            if (reviewDto.MovieIds != null)
            {
                var movie = await _unitOfWork.Movies.GetAsync(reviewDto.MovieIds);
                if (movie == null)
                    return NotFound($"Movie with ID {reviewDto.MovieIds} not found");

                review.Movie = movie; // Set navigation property
            }

            _unitOfWork.Reviews.Update(review);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostReview(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);

            if (reviewDto.MovieIds != null)
            {
                var movie = await _unitOfWork.Movies.GetAsync(reviewDto.MovieIds);
                if (movie == null)
                    return NotFound($"Movie with ID {reviewDto.MovieIds} not found");

                review.Movie = movie;
            }

            _unitOfWork.Reviews.Add(review);
            await _unitOfWork.CompleteAsync();

            var createdDto = _mapper.Map<ReviewDto>(review);
            return CreatedAtAction(nameof(GetReview), new { id = createdDto.Id }, createdDto);
        }

        // DELETE: api/reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null) return NotFound();

            _unitOfWork.Reviews.Remove(review);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<ReviewDto>(review));
        }

        private async Task<bool> ReviewExists(int id)
        {
            return await _unitOfWork.Reviews.AnyAsync(id);
        }
    }
}
