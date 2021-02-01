using AutoMapper;
using Generic.Api.Dtos;
using Generic.Core.Models;

namespace MyMusic.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<User, UserForReturnDto>();
            
            // Resource to Domain
            CreateMap<UserForCreateDto, User>();
        }
    }
}