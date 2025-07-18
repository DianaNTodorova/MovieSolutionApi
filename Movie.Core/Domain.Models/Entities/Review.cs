﻿namespace Movie.Core.Domain.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } 
        
        // Navigation property to link to the Movie
        public Movies Movie { get; set; } = null!;
    }
}
