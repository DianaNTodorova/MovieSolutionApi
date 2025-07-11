using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models;
using MovieApi.Models.DTOs;

namespace MovieApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieApiContext _context;
        private readonly IMapper _mapper;

        public ReviewsController(MovieApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        {
            var review= await _context.Review.ToListAsync();
            if (review == null || !review.Any())
            {
                return NotFound();
            }
           
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(review);
            return Ok(reviewDtos);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Review.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id)
            {
                return BadRequest();
            }

            var review = await _context.Review
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(a => a.Id == id);

            _mapper.Map(reviewDto, review);
            if(reviewDto.MovieIds !=null)
            {

                var movies = await _context.Movie
                    .Where(m => reviewDto.MovieIds.Contains(m.Id))
                    .ToListAsync();
                //review.Movie = movies;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Review.Add(review);
            await _context.SaveChangesAsync();
            var createReviewDto = _mapper.Map<ReviewDto>(review);
            return CreatedAtAction("GetReview", new { id = createReviewDto.Id }, createReviewDto);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            var deleteReviewDto = _mapper.Map<ReviewDto>(review);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(deleteReviewDto);
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
