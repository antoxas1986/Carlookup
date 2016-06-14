using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Entities;
using CarLookUp.Data.Repository;
using CarLookUp.UnitTest.Helpers;
using CarLookUp.Web.Mappers;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using Xunit;

namespace CarLookUp.UnitTest.Data.Repository
{
    public class RoleRepositoryUT
    {
        private Mock<ICarContext> _db;
        private DbSetHelper _helper;
        private List<Role> _roleList;
        private Mock<DbSet<Role>> _roleSet;
        private RoleRepo _sut;

        public RoleRepositoryUT()
        {
            _helper = new DbSetHelper();
            _db = new Mock<ICarContext>();
            _sut = new RoleRepo(_db.Object);
            _roleList = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name="Test"
                },
                new Role
                {
                    Id = 2,
                    Name ="Test"
                }
            };

            _roleSet = _helper.GetDbSet(_roleList);
            _db.Setup(c => c.Roles).Returns(_roleSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void GetAll_RoleDTO_Valid()
        {
            ICollection<RoleDTO> actual = _sut.GetAll();
            Assert.Equal(_roleList.Count, actual.Count);
        }

        [Fact]
        public void GetById_RoleDTO_Valid()
        {
            RoleDTO actual = _sut.GetById(1);
            Assert.Equal(actual.Id, _roleList[0].Id);
        }
    }
}
