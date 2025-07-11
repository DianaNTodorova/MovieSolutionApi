namespace MovieApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int Duration { get; set; } 

        //Navigaation property for 1:1 relation
        public MovieDetails MovieDetails { get; set; } = null!;

        //Navigation property for 1:N relation // ICollection... define a 1-to-many relationship 
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        //Navigation property for M:N relation
        public ICollection<Actor> Actor { get; set; } = new List<Actor>();
        public ICollection<MovieActor> MovieActors { get; set; }


    }
}
