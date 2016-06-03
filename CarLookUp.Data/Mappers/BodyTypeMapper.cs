using AutoMapper;
using CarLookUp.Core.Mappers.Interfaces;
using CarLookUp.Core.Models;
using CarLookUp.Data.Entities;

namespace CarLookUp.Data.Mappers
{
    public class BodyTypeMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<BodyType, BodyTypeDTO>();
        }
    }
}
