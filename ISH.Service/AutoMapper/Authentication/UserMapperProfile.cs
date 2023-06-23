using AutoMapper;
using ISH.Data.Authentication;
using ISH.Service.Dtos.Authentication;

namespace ISH.Service.AutoMapper.Authentication
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
