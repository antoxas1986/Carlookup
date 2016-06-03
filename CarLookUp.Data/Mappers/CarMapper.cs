using AutoMapper;
using CarLookUp.Core.Mappers.Interfaces;
using CarLookUp.Core.Models;
using CarLookUp.Data.Entities;

namespace CarLookUp.Data.Mappers
{
    public class CarMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Car, CarDTO>();
            Mapper.CreateMap<Car, CarDTOWithBodyTypeName>()
                .ForMember(dest => dest.BodyType, opts => opts.MapFrom(src => src.BodyType.TypeOfBody));
            Mapper.CreateMap<CarDTO, Car>();
        }
    }
}
