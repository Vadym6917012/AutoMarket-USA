using Application.DTOs.Modification;
using AutoMapper;
using Domain.Entities;

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
