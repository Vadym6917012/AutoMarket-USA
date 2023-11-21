using AutoMapper;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Shared.DTOs;
using AutoMarket.Server.Shared.DTOs.BodyType;
using AutoMarket.Server.Shared.DTOs.Car;
using AutoMarket.Server.Shared.DTOs.DriveTrain;
using AutoMarket.Server.Shared.DTOs.FuelType;
using AutoMarket.Server.Shared.DTOs.GearBox;
using AutoMarket.Server.Shared.DTOs.Generation;
using AutoMarket.Server.Shared.DTOs.Images;
using AutoMarket.Server.Shared.DTOs.Make;
using AutoMarket.Server.Shared.DTOs.Model;
using AutoMarket.Server.Shared.DTOs.ModelGeneration;
using AutoMarket.Server.Shared.DTOs.Modification;
using AutoMarket.Server.Shared.DTOs.ProducingCountry;
using AutoMarket.Server.Shared.DTOs.TechnicalCondition;

namespace AutoMarket.Server.Infrastructure
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<BodyType, BodyTypeDTO>().ReverseMap();

            CreateMap<Car, CarCreateDTO>().ReverseMap();

            CreateMap<Car, CarDTO>()
                .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.Model.Make.Name))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.ModificationName, opt => opt.MapFrom(src => src.Modification.Name))
                .ForMember(dest => dest.GenerationName, opt => opt.MapFrom(src => src.Generation.Name))
                .ForMember(dest => dest.BodyTypeName, opt => opt.MapFrom(src => src.BodyType.Name))
                .ForMember(dest => dest.GearBoxTypeName, opt => opt.MapFrom(src => src.GearBoxType.Name))
                .ForMember(dest => dest.DriveTrainName, opt => opt.MapFrom(src => src.DriveTrain.Name))
                .ForMember(dest => dest.TechnicalConditionName, opt => opt.MapFrom(src => src.TechnicalCondition.Name))
                .ForMember(dest => dest.FuelTypeName, opt => opt.MapFrom(src => src.FuelType.Name))
                .ForMember(dest => dest.ImagesPath, opt => opt.MapFrom(src => src.Images.Select(p => p.ImagePathToDisplay)))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            CreateMap<FuelType, FuelTypeDTO>().ReverseMap();
            CreateMap<GearBoxType, GearBoxTypeDTO>().ReverseMap();
            CreateMap<Generation, GenerationDTO>().ReverseMap();
            CreateMap<Images, ImagesDTO>().ReverseMap();
            CreateMap<Make, MakeDTO>()
                .ForMember(dest => dest.ModelsId, opt => opt.MapFrom(src => src.Models.Select(mi => mi.Id)))
                .ReverseMap();
            CreateMap<Model, ModelDTO>()
                .ForMember(dest => dest.Generations, opt => opt.MapFrom(src => src.ModelGenerations.Select(pc => pc.Generation.Name)))
                .ReverseMap();
            CreateMap<ModelGeneration, ModelGenerationDTO>().ReverseMap();
            CreateMap<Modification, ModificationDTO>().ReverseMap();
            CreateMap<ProducingCountry, ProducingCountryDTO>().ReverseMap();
            CreateMap<DriveTrain, DriveTrainDTO>().ReverseMap();
            CreateMap<TechnicalCondition, TechnicalConditionDTO>().ReverseMap();
        }
    }
}
