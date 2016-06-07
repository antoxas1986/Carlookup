using AutoMapper;
using CarLookUp.Core.Mappers.Interfaces;
using CarLookUp.Core.Models;
using CarLookUp.Data.Entities;

namespace CarLookUp.Services.Mappers
{
    public class CarMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Car, CarDTO>();
            Mapper.CreateMap<Car, CarDTOWithBodyType>();
        }
    }
}
