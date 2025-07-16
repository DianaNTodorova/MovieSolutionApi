namespace Movie.Core.Domain.Models.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string? ReviewerName { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int MovieIds { get; set; } 
    }
}
