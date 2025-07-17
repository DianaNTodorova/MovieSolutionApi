using Movie.Core.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<MovieDto?> GetMovieByIdAsync(int id);
        Task<MovieDetailsDto?> GetMovieDetailsAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto dto);
        Task<bool> UpdateMovieAsync(int id, MovieDto dto);
        Task<MovieDto?> DeleteMovieAsync(int id);
        Task AddActorToMovieAsync(int movieId, MovieActorCreateDto dto);
    }
}
