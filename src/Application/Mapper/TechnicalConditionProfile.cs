using Application.DTOs.TechnicalCondition;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class TechnicalConditionProfile : Profile
    {
        public TechnicalConditionProfile()
        {
            CreateMap<TechnicalCondition, TechnicalConditionDTO>().ReverseMap();
        }
    }
}
