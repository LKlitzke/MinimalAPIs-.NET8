using AutoMapper;

namespace MinimalAPIs.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genero, Genero>();
            CreateMap<Game, Game>();
        }
    }
}
