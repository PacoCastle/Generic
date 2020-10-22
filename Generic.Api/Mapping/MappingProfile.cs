using AutoMapper;
using Generic.Api.DTOS;
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
        }
    }
}