﻿namespace Movie.Core.Domain.Models.Entities
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Year { get; set; }
        public int Duration { get; set; } 

        //Navigaation property for 1:1 relation
        public MovieDetails MovieDetails { get; set; } = null!;

        //Navigation property for 1:N relation // ICollection... define a 1-to-many relationship 
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        //Navigation property for M:N relation
    
        public ICollection<MovieActor> MovieActors { get; set; }
        public int GenreId { get; set; }
        public Genre Genres { get; set; }


    }
}
