using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models;

public static class SeedDataExtensions
{
    public static void SeedData(this WebApplication app) //app.SeedData();
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MovieApiContext>();
        context.Database.Migrate();


        if (!context.Movie.Any())
        {
            context.Movie.AddRange(
            new Movie
            {
                Title = "Titanic",
                Year = new DateTime(2014, 4, 4),
                Duration = 2,
                Genre = "Sci-Fi",
                Reviews = new List<Review>
    {
        new Review { Rating = 5, Comment = "Amazing movie!" },
        new Review { Rating = 4, Comment = "Great plot!" }
    },
                Actor = new List<Actor>
    {
        new Actor { Name = "Leonardo DiCaprio", BirthYear = new DateTime(1969, 5, 5) },
        new Actor { Name = "Leonardo SomeOne", BirthYear = new DateTime(1969, 5, 5) }
    },
                MovieDetails = new MovieDetails { Synopsis = "Some great movie", Language = "English", Budget = 100000 }
            },
            new Movie
    {
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 2,
    Genre = "Sci-Fi",
    Reviews = new List<Review>
    {
        new Review { Rating = 5, Comment = "Mind-bending masterpiece!" },
        new Review { Rating = 4, Comment = "Creative and thrilling plot!" }
    },
    Actor = new List<Actor>
    {
        new Actor { Name = "Leonardo DiCaprio", BirthYear = new DateTime(1974, 11, 11) },
        new Actor { Name = "Joseph Gordon-Levitt", BirthYear = new DateTime(1981, 2, 17) }
    },
    MovieDetails = new MovieDetails { Synopsis = "A thief who enters the dreams of others to steal their secrets.", Language = "English", Budget = 160000000 }
},
new Movie
{
    Title = "The Matrix",
    Year = new DateTime(1999, 3, 31),
    Duration = 2,
    Genre = "Action",
    Reviews = new List<Review>
    {
        new Review { Rating = 5, Comment = "Revolutionary and mind-blowing!" },
        new Review { Rating = 4, Comment = "Changed the landscape of sci-fi action!" }
    },
    Actor = new List<Actor>
    {
        new Actor { Name = "Keanu Reeves", BirthYear = new DateTime(1964, 9, 2) },
        new Actor { Name = "Carrie-Anne Moss", BirthYear = new DateTime(1967, 8, 21) }
    },
    MovieDetails = new MovieDetails { Synopsis = "A computer hacker learns about the true nature of reality and his role in the war against its controllers.", Language = "English", Budget = 63000000 }
}

            );
         
            context.SaveChanges();
        }

   
    }
}
