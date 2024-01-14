using Application.DTOs.ProducingCountry;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class ProducingCountryProfile : Profile
    {
        public ProducingCountryProfile()
        {
            CreateMap<ProducingCountry, ProducingCountryDTO>().ReverseMap();
        }
    }
}
