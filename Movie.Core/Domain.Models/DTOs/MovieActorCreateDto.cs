using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Domain.Models.DTOs
{
    public class MovieActorCreateDto
    {
        [Required]
        public int ActorId { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
