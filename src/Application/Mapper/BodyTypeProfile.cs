using Application.BodyTypeMediatoR.Commands;
using Application.DTOs.BodyType;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class BodyTypeProfile : Profile
    {
        public BodyTypeProfile()
        {
            CreateMap<BodyType, BodyTypeDTO>().ReverseMap();
            CreateMap<BodyTypeDTO, CreateBodyType>().ReverseMap();
            CreateMap<BodyTypeDTO, UpdateBodyType>().ReverseMap();
        }
    }
}
