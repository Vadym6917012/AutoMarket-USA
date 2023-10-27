using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Shared.DTOs;
using AutoMarket.Server.Shared.DTOs.Car;

namespace AutoMarket.Server.Infrastructure
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<BodyType, BodyTypeDTO>().ReverseMap();

            CreateMap<Car, CarCreateDTO>().ReverseMap();

            CreateMap<Car, CarDTO>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.ModificationName, opt => opt.MapFrom(src => src.Modification.Name))
                .ForMember(dest => dest.BodyTypeName, opt => opt.MapFrom(src => src.BodyType.Name))
                .ForMember(dest => dest.GearBoxTypeName, opt => opt.MapFrom(src => src.GearBoxType.Name))
                .ForMember(dest => dest.FuelTypeName, opt => opt.MapFrom(src => src.FuelType.Name))
                .ForMember(dest => dest.ImagesPath, opt => opt.MapFrom(src => src.Images.Select(p => p.ImagePathToDisplay)));
             
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
