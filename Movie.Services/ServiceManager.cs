using AutoMapper;
using Movie.Core.Domain.Contracts;
using Movie.Service.Contracts;
using System;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IActorService> _actorService;
        private readonly Lazy<IReviewService> _reviewService;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _actorService = new Lazy<IActorService>(() => new ActorService(unitOfWork, mapper));
            _reviewService = new Lazy<IReviewService>(() => new ReviewService(unitOfWork, mapper));
            _movieService = new Lazy<IMovieService>(() => new MovieService(unitOfWork, mapper));
        }

        public IActorService ActorService => _actorService.Value;
        public IReviewService ReviewService => _reviewService.Value;
        public IMovieService MovieService => _movieService.Value;

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _unitOfWork.SaveAsync();
            return true; // You can customize this based on actual logic if needed.
        }
    }
}
