using Microsoft.EntityFrameworkCore;
using Movie.Core.Domain.Models.Entities;
//using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace MovieApi.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Actor> Actor { get; set; } = default!;
        public DbSet<MovieDetails> MovieDetails { get; set; } = default!;
        public DbSet<Review> Review { get; set; } = default!;
        public DbSet<Movies> Movies { get; set; } = default!;
        public DbSet<MovieActor> MovieActor { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // preventing creation of Ids
            //modelBuilder.Entity<Movie>().Property(m => m.Id).ValueGeneratedNever();
            //modelBuilder.Entity<Actor>().Property(a => a.Id).ValueGeneratedNever();
            //modelBuilder.Entity<MovieDetails>().Property(md => md.MovieId).ValueGeneratedNever();
            //modelBuilder.Entity<Review>().Property(r => r.Id).ValueGeneratedNever();

            // Configure the many-to-many relationship between Movie and Actor
            modelBuilder.Entity<Movies>()
                .HasMany(a => a.Actor)
                .WithMany(m => m.Movie);

            // Configure the one-to-one relationship between Movie and MovieDetails
            modelBuilder.Entity<Movies>()
                .HasOne(m => m.MovieDetails)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetails>(md => md.MovieId);
            // Configure the one-to-many relationship between Movie and Review
            modelBuilder.Entity<Movies>()
                .HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MovieApiContext;Trusted_Connection=True;ConnectRetryCount=0");
            }

        }
    }
}
