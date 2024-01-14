using Application.DTOs.GearBox;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class GearBoxTypeProfile : Profile
    {
        public GearBoxTypeProfile()
        {
            CreateMap<GearBoxType, GearBoxTypeDTO>().ReverseMap();
        }
    }
}
