using Application.DTOs.ProducingCountry;
using AutoMapper;

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
