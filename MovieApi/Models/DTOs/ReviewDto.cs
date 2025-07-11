namespace MovieApi.Models.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string? ReviewerName { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public List<int> MovieIds { get; set; } = new List<int>();
    }
}
