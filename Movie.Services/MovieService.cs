using AutoMapper;
using Movie.Core.Domain.Contracts;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using Movie.Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetAsync(id);
            return movie != null ? _mapper.Map<MovieDto>(movie) : null;
        }

        public async Task<MovieDetailsDto?> GetMovieDetailsAsync(int id)
        {
            return await _unitOfWork.Movies.GetMovieDetailsDtoAsync(id);
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto dto)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(dto.GenreId);

            if (genre == null)
            {
                throw new ArgumentException($"Genre with ID {dto.GenreId} does not exist.");
            }
            var movie = _mapper.Map<Movies>(dto);
            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> UpdateMovieAsync(int id, MovieDto dto)
        {
            var existingMovie = await _unitOfWork.Movies.GetAsync(id);
            if (existingMovie == null)
            {
                return false;
            }

            _mapper.Map(dto, existingMovie);
            _unitOfWork.Movies.Update(existingMovie);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<MovieDto?> DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetAsync(id);
            if (movie == null)
            {
                return null;
            }

            _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task AddActorToMovieAsync(int movieId, MovieActorCreateDto dto)
        {
            var movie = await _unitOfWork.Movies.GetAsync(movieId);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {movieId} not found.");
            }

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = dto.ActorId,
                Role = dto.Role
            };

            await _unitOfWork.MovieActors.AnyAsync(movieActor);
            await _unitOfWork.SaveAsync();
        }
    }
}
