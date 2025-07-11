using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthYear { get; set; }
        public int MovieId { get; set; }
        public ICollection<Movie> Movie { get; set; } = new List<Movie>();
        public ICollection<MovieActor> MovieActors { get; set; }


    }
}
