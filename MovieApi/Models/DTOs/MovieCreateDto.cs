using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieCreateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "The title needs to be at leats 3 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime Year { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "The genre needs to be at least 3 characters")]
        public string Genre { get; set; } = string.Empty;
        [Required]
        [Range(100, 300, ErrorMessage = "The duration must be between 100 and 300 minutes")]
        public int Duration { get; set; }

        public string Synopsis { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int Budget { get; set; }

        //public MovieDetailsDto MovieDetails { get; set; } = null!;
        //public List<ReviewDto> ReviewDto { get; set; } = new();
        //public List<ActorDto> ActorDto { get; set; } = new();

    }
}
