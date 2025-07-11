namespace MovieApi.Models
{
    public class MovieActor
    {

        public Movie Movie { get; set; } = null!;
        public Actor Actor { get; set; } = null!;
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Role { get; set; }


    }
}
