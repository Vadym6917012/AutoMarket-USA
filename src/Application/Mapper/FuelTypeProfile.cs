using Application.DTOs.FuelType;
using AutoMapper;
using Domain.Entities;

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
