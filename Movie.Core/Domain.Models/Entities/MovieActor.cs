namespace Movie.Core.Domain.Models.Entities
{
    public class MovieActor
    {

        public Movies Movie { get; set; } = null!;
        public Actor Actor { get; set; } = null!;
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Role { get; set; }


    }
}
