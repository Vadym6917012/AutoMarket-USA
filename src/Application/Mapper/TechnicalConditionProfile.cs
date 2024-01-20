using Application.DTOs.TechnicalCondition;
using AutoMapper;

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
