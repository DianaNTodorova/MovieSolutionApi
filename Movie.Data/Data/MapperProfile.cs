using AutoMapper;
using Movie.Core.Domain.Models.DTOs;
using Movie.Core.Domain.Models.Entities;
using System.Linq;

namespace MovieApi.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Map from Movies → MovieDto (for returning data)
            CreateMap<Movies, MovieDto>()
                .ForMember(dest => dest.MovieDetails, opt => opt.MapFrom(src => src.MovieDetails))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                // Map actors through MovieActors join table
                .ForMember(dest => dest.Actor, opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Actor)))
                .ReverseMap();

            // Map for creating a Movie (MovieCreateDto → Movies)
            CreateMap<MovieCreateDto, Movies>()
                .ForMember(dest => dest.MovieDetails, opt => opt.MapFrom(src => new MovieDetails
                {
                    Synopsis = src.Synopsis,
                    Language = src.Language,
                    Budget = src.Budget
                }))
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.MovieActors, opt => opt.Ignore());  // Ignore for now; handle actor linking separately

            // Map back Movies → MovieCreateDto
            CreateMap<Movies, MovieCreateDto>()
                .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.MovieDetails.Synopsis))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MovieDetails.Language))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.MovieDetails.Budget))
                ;
            CreateMap<Movies, MovieDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genres.Name));


            // Simple mappings between Actor and ActorDto
            CreateMap<Actor, ActorDto>().ReverseMap();

            // Simple mappings between MovieDetails and MovieDetailsDto
            CreateMap<MovieDetails, MovieDetailsDto>().ReverseMap();

            // Simple mappings between Review and ReviewDto
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
