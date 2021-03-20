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
            CreateMap<Client, ClientReturn>();
            
            // Resource to Domain
            CreateMap<ClientCreate, Client>();
			
			// Domain to Resource
            CreateMap<User, UserForReturnDto>();
            
            // Resource to Domain
            CreateMap<UserForCreateDto, User>();

            // Resource to Domain
            CreateMap<UserForUpdateDto, User>();
        }
    }
}