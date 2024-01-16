using Application.DriveTrainMediatoR.Commands;
using Application.DTOs.DriveTrain;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class DriveTrainProfile : Profile
    {
        public DriveTrainProfile()
        {
            CreateMap<DriveTrain, DriveTrainDTO>().ReverseMap();
            CreateMap<DriveTrainDTO, CreateDriveTrain>().ReverseMap();
            CreateMap<DriveTrainDTO, UpdateDriveTrain>().ReverseMap();
        }
    }
}
