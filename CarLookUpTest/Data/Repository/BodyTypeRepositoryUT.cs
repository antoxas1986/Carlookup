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
    public class BodyTypeRepositoryUT
    {
        private List<BodyType> _bodyTypeList;
        private Mock<DbSet<BodyType>> _bodyTypeSet;
        private Mock<ICarContext> _db;
        private DbSetHelper _helper;
        private BodyTypeRepository _sut;

        public BodyTypeRepositoryUT()
        {
            _helper = new DbSetHelper();
            _db = new Mock<ICarContext>();
            _sut = new BodyTypeRepository(_db.Object);
            _bodyTypeList = new List<BodyType>
            {
                new BodyType
                {
                    Id = 1,
                    TypeOfBody ="Test",
                    Cars = new List<Car>
                    {
                        new Car(),
                        new Car()
                    }
                },
                new BodyType
                {
                    Id = 2,
                    TypeOfBody ="Test",
                    Cars = new List<Car>
                    {
                        new Car(),
                        new Car()
                    }
                }
            };

            _bodyTypeSet = _helper.GetDbSet(_bodyTypeList);
            _db.Setup(c => c.BodyTypes).Returns(_bodyTypeSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void GetAll_BodyTypeDTO_Valid()
        {
            ICollection<BodyTypeDTO> actual = _sut.GetAll();
            Assert.Equal(_bodyTypeList.Count, actual.Count);
        }

        [Fact]
        public void GetById_BodyTypeDTO_Valid()
        {
            BodyTypeDTO actual = _sut.GetById(1);
            Assert.Equal(actual.Id, _bodyTypeList[0].Id);
        }
    }
}
