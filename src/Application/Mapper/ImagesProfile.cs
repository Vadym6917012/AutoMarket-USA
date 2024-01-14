using Application.DTOs.Images;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class ImagesProfile : Profile
    {
        public ImagesProfile()
        {
            CreateMap<Images, ImagesDTO>().ReverseMap();
        }
    }
}
