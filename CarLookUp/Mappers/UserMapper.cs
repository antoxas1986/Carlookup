using AutoMapper;
using CarLookUp.Core.Mappers.Interfaces;
using CarLookUp.Core.Models;
using CarLookUp.Web.ViewModels;

namespace CarLookUp.Web.Mappers
{
    public class UserMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            //Mapper.CreateMap<UserVM, UserDTO>()
            //    .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src));

            //Mapper.CreateMap<UserVM, RoleDTO>()
            //    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.RoleId));

            //another solution
            Mapper.CreateMap<UserVM, UserDTO>()
                .ForMember(dest => dest.Role, opts => opts.ResolveUsing(src => new RoleDTO { Id = src.RoleId }));

            Mapper.CreateMap<UserDTO, UserVM>();
        }
    }
}
