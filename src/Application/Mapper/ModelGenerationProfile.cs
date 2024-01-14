using Application.DTOs.ModelGeneration;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class ModelGenerationProfile : Profile
    {
        public ModelGenerationProfile()
        {
            CreateMap<ModelGeneration, ModelGenerationDTO>().ReverseMap();
        }
    }
}
