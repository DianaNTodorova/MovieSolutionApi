using Microsoft.EntityFrameworkCore;
using Movie.Core.Domain.Models.Entities;

namespace MovieApi.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; } = default!;
        public DbSet<MovieDetails> MovieDetails { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Movies> Movies { get; set; } = default!;
        public DbSet<MovieActor> MovieActors { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-one relationship between Movies and MovieDetails
            modelBuilder.Entity<Movies>()
                .HasOne(m => m.MovieDetails)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetails>(md => md.MovieId);

            // One-to-many relationship between Movies and Reviews
            modelBuilder.Entity<Movies>()
                .HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

            // Configure many-to-many via join entity MovieActor
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });  // Composite Key

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);
            modelBuilder.Entity<Movies>()
                 .HasOne(m => m.Genres)
                 .WithMany(g => g.Movies)
                 .HasForeignKey(m => m.GenreId);
                
        }
    }
}
