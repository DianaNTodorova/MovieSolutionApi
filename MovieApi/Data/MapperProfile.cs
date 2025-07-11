using AutoMapper;
using MovieApi.Models;
using MovieApi.Models.DTOs;

namespace MovieApi.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Map from Movie → MovieDto (for returning data)
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.MovieDetails, opt => opt.MapFrom(src => src.MovieDetails))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Actor, opt => opt.MapFrom(src => src.Actor))
                .ReverseMap();

            // Map for creating a Movie (MovieCreateDto → Movie) //this needs more revision 
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.MovieDetails, opt => opt.MapFrom(src => new MovieDetails
                {
                    Synopsis = src.Synopsis,
                    Language = src.Language,
                    Budget = src.Budget
                }))
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.Actor, opt => opt.Ignore());

            CreateMap<Movie, MovieCreateDto>()
                .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.MovieDetails.Synopsis))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MovieDetails.Language))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.MovieDetails.Budget));

            
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<MovieDetails, MovieDetailsDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
    

