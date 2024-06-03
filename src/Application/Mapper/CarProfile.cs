using Application.Cars.Commands;
using Application.Cars.Queries;
using Application.DTOs.Car;
using AutoMapper;

namespace Application.Mapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
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
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.UserPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
            CreateMap<CreateCar, CarCreateDTO>().ReverseMap();
            CreateMap<UpdateCar, CarCreateDTO>().ReverseMap();
            CreateMap<CarAdvanceFilter, CarFilter>().ReverseMap();
            CreateMap<CarAdvanceFilter, CarHomeFilter>().ReverseMap();
            CreateMap<VinLookupResponse, VinCheckResponse>().ReverseMap();
        }
    }
}
