using Application.DTOs.Model;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Model, ModelDTO>()
                    .ForMember(dest => dest.Generations, opt => opt.MapFrom(src => src.ModelGenerations.Select(pc => pc.Generation.Name)))
                    .ReverseMap();
        }
    }
}
