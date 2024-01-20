using Application.DTOs.GearBox;
using AutoMapper;

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
