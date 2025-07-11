using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs
{
    public class MovieActorCreateDto
    {
        [Required]
        public int ActorId { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
