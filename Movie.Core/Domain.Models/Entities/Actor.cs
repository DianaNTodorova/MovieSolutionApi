using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Core.Domain.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthYear { get; set; }
        public int MovieId { get; set; }
        public ICollection<Movies> Movie { get; set; } = new List<Movies>();
        public ICollection<MovieActor> MovieActors { get; set; }


    }
}
