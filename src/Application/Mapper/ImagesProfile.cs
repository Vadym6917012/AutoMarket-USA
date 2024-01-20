using Application.DTOs.Images;
using AutoMapper;

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
