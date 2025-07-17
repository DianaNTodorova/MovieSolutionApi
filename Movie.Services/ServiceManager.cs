using AutoMapper;
using Microsoft.Extensions.Logging;
using Movie.Core.Domain.Contracts;
using Movie.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IActorService> _actorService;
        private readonly Lazy<IReviewService> _reviewService;
        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _actorService = new Lazy<IActorService>(() => new ActorService(unitOfWork, mapper));
            _reviewService = new Lazy<IReviewService>(() => new ReviewService(unitOfWork, mapper));
            _movieService = new Lazy<IMovieService>(() => new MovieService(unitOfWork, mapper));
        }
        public IActorService ActorService => _actorService.Value;
        public IReviewService ReviewService => _reviewService.Value;
        public IMovieService MovieService => _movieService.Value;

        public Task SaveAsync()
        {
            return SaveChangesAsync();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
