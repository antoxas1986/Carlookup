using CarLookUp.Core.Models;
using CarLookUp.Data.Context.Interfaces;
using CarLookUp.Data.Entities;
using CarLookUp.Data.Repository;
using CarLookUp.UnitTest.Helpers;
using CarLookUp.Web.Mappers;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace CarLookUp.UnitTest.Data.Repository
{
    public class CarRepositoryUT
    {
        private List<Car> _carList;
        private Mock<DbSet<Car>> _carSet;
        private Mock<ICarContext> _db;
        private DbSetHelper _helper;
        private CarRepository _sut;

        public CarRepositoryUT()
        {
            _helper = new DbSetHelper();
            _db = new Mock<ICarContext>();
            _sut = new CarRepository(_db.Object);
            _carList = new List<Car>
            {
                new Car
                {
                    Id = 1,
                Maker = "Test",
                Model = "Test",
                Year = 2005
                },
                new Car
                {
                    Id = 2,
                Maker = "Test",
                Model = "Test",
                Year = 2006
                }
            };

            _carSet = _helper.GetDbSet(_carList);
            _db.Setup(c => c.Cars).Returns(_carSet.Object);

            AutoMapperConfig.Execute();
        }

        //[Fact]
        public void AddCar_CarDTO_Valid()
        {
            CarDTOWithBodyType car = new CarDTOWithBodyType
            {
                Id = 5,
                Maker = "Test",
                Model = "Test",
                Year = 2005,
                BodyTypeId = 1
            };
            _sut.AddCar(car);
            _db.Object.SaveChanges();

            //_db.Setup(r => r.Cars.Add(It.IsAny<Car>())).Returns(car);
            //_db.Verify(d => d.Cars.Add(It.IsAny<Car>()), Times.Once());

            Car actual = _db.Object.Cars.Where(c => c.Id == 5).FirstOrDefault();
            Assert.Equal(actual.Id, car.Id);
        }

        [Fact]
        public void GetAll_CarDTO_Valid()
        {
            ICollection<CarDTO> actual = _sut.GetAll<CarDTO>();
            Assert.Equal(_carList.Count, actual.Count);
        }

        [Fact]
        public void GetCar_CarDTO_Valid()
        {
            CarDTO actual = _sut.GetCar<CarDTO>(1);
            Assert.Equal(actual.Id, _carList[0].Id);
        }
    }
}
