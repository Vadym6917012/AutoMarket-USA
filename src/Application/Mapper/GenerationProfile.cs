using Application.DTOs.Generation;
using AutoMapper;
using Domain.Entities;

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
