using Application.DTOs.Generation;
using AutoMapper;

namespace Application.Mapper
{
    public class GenerationProfile : Profile
    {
        public GenerationProfile()
        {
            CreateMap<Generation, GenerationDTO>().ReverseMap();
        }
    }
}
