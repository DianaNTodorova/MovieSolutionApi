using Microsoft.AspNetCore.Mvc;
using Movie.Core.Domain.Models.DTOs;
using Movie.Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ReviewsController(IServiceManager service)
        {
            _service = service;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = await _service.ReviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/reviews/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _service.ReviewService.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        // PUT: api/reviews/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                await _service.ReviewService.UpdateReviewAsync(reviewDto);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostReview(ReviewDto reviewDto)
        {
            try
            {
                await _service.ReviewService.AddReviewAsync(reviewDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

            return CreatedAtAction(nameof(GetReview), new { id = reviewDto.Id }, reviewDto);
        }

        // DELETE: api/reviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await _service.ReviewService.DeleteReviewAsync(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
