using Application.DTOs.Make;
using AutoMapper;

namespace Application.Mapper
{
    public class MakeProfile : Profile
    {
        public MakeProfile()
        {
            CreateMap<Make, MakeDTO>()
                .ForMember(dest => dest.ModelsId, opt => opt.MapFrom(src => src.Models.Select(mi => mi.Id)))
                .ReverseMap();
        }
    }
}
