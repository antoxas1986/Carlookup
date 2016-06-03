using AutoMapper;
using CarLookUp.Core.Mappers.Interfaces;
using CarLookUp.Core.Models;
using CarLookUp.Web.ViewModels;

namespace CarLookUp.Web.Mappers
{
    public class CarMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<CarDTO, CarVM>();
            Mapper.CreateMap<CarVM, CarDTO>();
            Mapper.CreateMap<CarDTOWithBodyTypeName, CarVMWithBodyTypeName>();
        }
    }
}
