using Application.DTOs.Modification;
using Application.Modifications.Commands;
using AutoMapper;

namespace Application.Mapper
{
    public class ModificationProfile : Profile
    {
        public ModificationProfile()
        {
            CreateMap<Modification, ModificationDTO>().ReverseMap();
            CreateMap<CreateModification, ModificationDTO>().ReverseMap();
        }
    }
}
