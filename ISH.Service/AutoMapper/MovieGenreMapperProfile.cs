using AutoMapper;
using ISH.Data;
using ISH.Service.Dtos;

namespace ISH.Service.AutoMapper
{
    public class MovieGenreMapperProfile : Profile
    {
        public MovieGenreMapperProfile()
        {
            CreateMap<MovieGenre, MovieGenreDto>();
            CreateMap<MovieGenreDto, MovieGenre>();
        }
    }
}
