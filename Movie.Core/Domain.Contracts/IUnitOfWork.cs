using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

        IMovieRepository Movies { get; }
        IActorRepository Actors { get; }
        IReviewRepository Reviews { get; }
        IMovieActorRepository MovieActors { get; }
    }
}
