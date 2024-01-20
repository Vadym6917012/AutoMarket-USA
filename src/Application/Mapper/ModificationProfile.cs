using Application.DTOs.Modification;
using AutoMapper;

namespace Application.Mapper
{
    public class ModificationProfile : Profile
    {
        public ModificationProfile()
        {
            CreateMap<Modification, ModificationDTO>().ReverseMap();
        }
    }
}
