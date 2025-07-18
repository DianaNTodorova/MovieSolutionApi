using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie.Core.Domain.Models.Entities;
using MovieApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

public static class SeedDataExtensions
{
    public static void SeedData(this WebApplication app) // call with app.SeedData();
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

        context.Database.Migrate();

        // Seed genres with explicit IDs if none exist
        if (!context.Genres.Any())
        {
            context.Genres.AddRange(
                new Genre { Id = 1, Name = "Documentary" },
                new Genre { Id = 2, Name = "Sci-Fi" },
                new Genre { Id = 3, Name = "Action" }
            );
            context.SaveChanges();
        }

        // Seed movies if none exist, using explicit genre IDs
        if (!context.Movies.Any())
        {
            context.Movies.AddRange(
                new Movies
                {
                    Title = "Titanic",
                    Year = new DateTime(2014, 4, 4),
                    Duration = 2,
                    GenreId = 3, // Action
                    Reviews = new List<Review>
                    {
                        new Review { Rating = 5, Comment = "Amazing movie!" },
                        new Review { Rating = 4, Comment = "Great plot!" }
                    },
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = "Some great movie",
                        Language = "English",
                        Budget = 100000
                    },
                    MovieActors = new List<MovieActor>
                    {
                        new MovieActor
                        {
                            Actor = new Actor { Name = "Leonardo DiCaprio", BirthYear = new DateTime(1969, 5, 5) },
                            Role = "Jack Dawson"
                        },
                        new MovieActor
                        {
                            Actor = new Actor { Name = "Leonardo SomeOne", BirthYear = new DateTime(1969, 5, 5) },
                            Role = "Supporting Actor"
                        }
                    }
                },
                new Movies
                {
                    Title = "Inception",
                    Year = new DateTime(2010, 7, 16),
                    Duration = 2,
                    GenreId = 2, // Sci-Fi
                    Reviews = new List<Review>
                    {
                        new Review { Rating = 5, Comment = "Mind-bending masterpiece!" },
                        new Review { Rating = 4, Comment = "Creative and thrilling plot!" }
                    },
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = "A thief who enters the dreams of others to steal their secrets.",
                        Language = "English",
                        Budget = 160000000
                    },
                    MovieActors = new List<MovieActor>
                    {
                        new MovieActor
                        {
                            Actor = new Actor { Name = "Leonardo DiCaprio", BirthYear = new DateTime(1974, 11, 11) },
                            Role = "Dom Cobb"
                        },
                        new MovieActor
                        {
                            Actor = new Actor { Name = "Joseph Gordon-Levitt", BirthYear = new DateTime(1981, 2, 17) },
                            Role = "Arthur"
                        }
                    }
                },
                new Movies
                {
                    Title = "The Matrix",
                    Year = new DateTime(1999, 3, 31),
                    Duration = 2,
                    GenreId = 1, // Documentary
                    Reviews = new List<Review>
                    {
                        new Review { Rating = 5, Comment = "Revolutionary and mind-blowing!" },
                        new Review { Rating = 4, Comment = "Changed the landscape of sci-fi action!" }
                    },
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = "A computer hacker learns about the true nature of reality and his role in the war against its controllers.",
                        Language = "English",
                        Budget = 63000000
                    },
                    MovieActors = new List<MovieActor>
                    {
                        new MovieActor
                        {
                            Actor = new Actor { Name = "Keanu Reeves", BirthYear = new DateTime(1964, 9, 2) },
                            Role = "Neo"
                        },
                        new MovieActor
                        {
                            Actor = new Actor { Name = "Carrie-Anne Moss", BirthYear = new DateTime(1967, 8, 21) },
                            Role = "Trinity"
                        }
                    }
                }
            );

            context.SaveChanges();
        }
    }
}
