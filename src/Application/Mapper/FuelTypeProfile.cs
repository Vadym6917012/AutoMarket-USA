using Application.DTOs.FuelType;
using AutoMapper;

namespace Application.Mapper
{
    public class FuelTypeProfile : Profile
    {
        public FuelTypeProfile()
        {
            CreateMap<FuelType, FuelTypeDTO>().ReverseMap();
        }
    }
}
