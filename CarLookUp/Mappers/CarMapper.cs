using AutoMapper;
using CarLookUp.Core.Mappers.Interfaces;
using CarLookUp.Core.Models;
using CarLookUp.Web.ViewModels;
using System.Web.Mvc;

namespace CarLookUp.Web.Mappers
{
    public class CarMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<CarDTO, CarVM>();
            Mapper.CreateMap<CarVM, CarDTO>();
            Mapper.CreateMap<CarDTOWithBodyType, CarVMWithBodyTypeName>();
            Mapper.CreateMap<CarVMWithBodyTypeName, CarDTOWithBodyType>();
            Mapper.CreateMap<BodyTypeDTO, SelectListItem>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.TypeOfBody));
        }
    }
}
