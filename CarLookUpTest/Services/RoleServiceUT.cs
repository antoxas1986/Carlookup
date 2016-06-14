using CarLookUp.Core.Models;
using CarLookUp.Data.Repository.Interfaces;
using CarLookUp.Services;
using CarLookUp.Web.Mappers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CarLookUp.UnitTest.Services
{
    public class RoleServiceUT
    {
        private Mock<IRoleRepo> _roleRepo;
        private RoleService _sut;

        public RoleServiceUT()
        {
            _roleRepo = new Mock<IRoleRepo>();
            _sut = new RoleService(_roleRepo.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void GetAll_RoleDTO_Valid()
        {
            ICollection<RoleDTO> _list = new List<RoleDTO>
            {
                new RoleDTO(),
                new RoleDTO()
            };
            _roleRepo.Setup(b => b.GetAll()).Returns(_list);
            ICollection<RoleDTO> actual = _sut.GetAll();
            Assert.Equal(actual.Count, _list.Count);
        }

        [Fact]
        public void GetById_RoleDTO()
        {
            RoleDTO dto = new RoleDTO
            {
                Id = 1,
                Name = "Test"
            };
            _roleRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(dto);
            RoleDTO actual = _sut.GetById(1);
            Assert.Equal(dto.Id, actual.Id);
        }
    }
}
