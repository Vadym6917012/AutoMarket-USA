using Application.DTOs.Model;
using Application.Models.Commands;
using AutoMapper;

namespace Application.Mapper
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Model, ModelDTO>()
                    .ForMember(dest => dest.Generations, opt => opt.MapFrom(src => src.ModelGenerations.Select(pc => pc.Generation.Name)))
                    .ReverseMap();
            CreateMap<CreateModel, ModelDTO>().ReverseMap();
        }
    }
}
