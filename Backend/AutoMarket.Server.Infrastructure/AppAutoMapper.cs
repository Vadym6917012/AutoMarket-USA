using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.Extensions.Configuration;

namespace AutoMarket.Server.Infrastructure
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<BodyType, BodyTypeDTO>().ReverseMap();
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<FuelType, FuelTypeDTO>().ReverseMap();
            CreateMap<GearBoxType, GearBoxTypeDTO>().ReverseMap();
            CreateMap<Generation, GenerationDTO>().ReverseMap();
            CreateMap<Images, ImagesDTO>().ReverseMap();
            CreateMap<Make, MakeDTO>().ReverseMap();
            CreateMap<Model, ModelDTO>()
                .ForMember(dest => dest.Generations, opt => opt.MapFrom(src => src.ModelGenerations.Select(pc => pc.Generation.Name)))
                .ReverseMap();
            CreateMap<ModelGeneration, ModelGenerationDTO>().ReverseMap();
            CreateMap<Modification, ModificationDTO>().ReverseMap();
            CreateMap<ProducingCountry, ProducingCountryDTO>().ReverseMap();
        }
    }
}
