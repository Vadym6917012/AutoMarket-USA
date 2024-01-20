using Application.DTOs.ModelGeneration;
using AutoMapper;

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
