using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Extensions
{
    public static class ModelBuilderExtensions
    {
      public static async Task SeedDataAsync(this ModelBuilder modelBuilder)
      {
          // Seed Actors
          modelBuilder.Entity<Actor>().HasData(
              new Actor { Id = 1, Name = "Leonardo DiCaprio", BirthYear = new DateTime(1974, 11, 11) },
              new Actor { Id = 2, Name = "Kate Winslet", BirthYear = new DateTime(1975, 10, 5) }
          );
          // Seed Movies
          modelBuilder.Entity<Movie>().HasData(
              new Movie { Id = 1, Title = "Titanic", Year = new DateTime(1997, 12, 19) },
              new Movie { Id = 2, Title = "Inception", Year = new DateTime(2010, 7, 16) }
          );
          // Seed MovieDetails
          modelBuilder.Entity<MovieDetails>().HasData(
              new MovieDetails { MovieId = 1, Synopsis = "A love story set on the ill-fated Titanic.", Language = "English", Budget = 200000000 },
              new MovieDetails { MovieId = 2, Synopsis = "A thief who steals corporate secrets through dream-sharing technology.", Language = "English", Budget = 160000000 }
          );
          // Seed MovieActors
          modelBuilder.Entity<MovieActor>().HasData(
              new MovieActor { MovieId = 1, ActorId = 1 },
              new MovieActor { MovieId = 1, ActorId = 2 },
              new MovieActor { MovieId = 2, ActorId = 1 }
          );
           
        }

    }
}
