using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Domain.Contracts
{
    public interface IMovieRepository
    {
         Task<IEnumerable<Movies>> GetAllAsync();
         Task<Movies>? GetAsync(int id);
         Task <bool>AnyAsync(int id);
         Task <MovieDetailsDto>? GetMovieDetailsDtoAsync(int id);
         void Add(Movies movie);
         void Update(Movies movie);
         void Remove(Movies movie);

    }
}
