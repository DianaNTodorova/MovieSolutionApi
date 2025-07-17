using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Contracts
{
    public  interface IServiceManager
    {
        IMovieService MovieService { get; }
        IActorService ActorService { get; }
        IReviewService ReviewService { get; }
        Task SaveAsync();
        Task<bool> SaveChangesAsync();
    }
}
