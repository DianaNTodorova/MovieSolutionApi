using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Core.Domain.Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movies>> GetAllAsync();
        Task<Movies> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        Task<MovieDetailsDto> GetMovieDetailsDtoAsync(int id);

        Task AddAsync(Movies movie);
        void Update(Movies movie);
        Task DeleteAsync(int id);
        void Remove(Movies movie);
    }
}
