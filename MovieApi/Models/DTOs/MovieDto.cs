namespace MovieApi.Models.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int Duration { get; set; }

        public MovieDetailsDto MovieDetails { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
        public List<ActorDto> Actor { get; set; } = new();
    }
}
