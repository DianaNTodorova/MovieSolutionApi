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
            IMovieActorRepository movieActorRepository)
        {
            _context = context;
            Movies = movieRepository;
            Actors = actorRepository;
            Reviews = reviewRepository;
            MovieActors = movieActorRepository;

        }
        public IMovieRepository Movies { get; }

        public IActorRepository Actors { get; }

        public IReviewRepository Reviews { get; }
        public IMovieActorRepository MovieActors { get; }

        public async Task CompleteAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}
