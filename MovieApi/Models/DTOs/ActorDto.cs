using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class ActorDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "The title needs to be at leats 2 characters")]
        public string Name { get; set; }
        [Required]
        public DateTime BirthYear { get; set; }
        public List<int> MovieIds { get; set; } = new List<int>();
    }
}
