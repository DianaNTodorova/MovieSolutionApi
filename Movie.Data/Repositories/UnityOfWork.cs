using Movie.Core.Domain.Contracts;
using MovieApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data.Repositories
{
    public class UnityOfWork : IUnitOfWork
    {
        private readonly MovieDbContext _context;
        public UnityOfWork(MovieDbContext context,
            IMovieRepository movieRepository,
            IActorRepository actorRepository,
            IReviewRepository reviewRepository,
            IMovieActorRepository movieActorRepository,
            IGenreRepository genreRepository)

        {
            _context = context;
            Movies = movieRepository;
            Actors = actorRepository;
            Reviews = reviewRepository;
            MovieActors = movieActorRepository;
            Genres = genreRepository;
        }
        public IMovieRepository Movies { get; }

        public IActorRepository Actors { get; }

        public IReviewRepository Reviews { get; }
        public IMovieActorRepository MovieActors { get; }

        public IGenreRepository Genres { get; }

        public async Task CompleteAsync()
        {
             await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
