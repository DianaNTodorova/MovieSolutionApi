﻿using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Domain.Models.DTOs
{
    public class MovieDetailsDto
    {
        [Required]
        [StringLength(100)]
        public string Synopsis { get; set; } = string.Empty;
        [Required]
        public string Language { get; set; } = string.Empty;
        [Range(1000000, double.MaxValue, ErrorMessage = "The budget must be at least 1000,000")]
        public decimal Budget { get; set; }
        public List<ActorDto>? Actors { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
    }
}
