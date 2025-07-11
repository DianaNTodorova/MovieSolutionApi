using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieUpdateDto
    {
        [MinLength(3, ErrorMessage = "The title needs to be at leats 3 characters")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Range(100, 300, ErrorMessage = "The duration must be between 100 and 300 minutes")]
        public int Duration { get; set; }
        public string Genre { get; set; } = string.Empty;
        public List<int> ActorIds { get; set; } = new List<int>();


    }
}
