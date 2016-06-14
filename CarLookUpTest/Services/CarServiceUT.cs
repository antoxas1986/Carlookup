using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Data.Repository.Interfaces;
using CarLookUp.Services;
using CarLookUp.Web.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarLookUp.UnitTest.Services
{
    public class CarServiceUT
    {
        private Mock<IBodyTypeRepository> _bodyRepo;
        private ICollection<CarDTOWithBodyType> _carList;
        private Mock<ICarRepository> _carRepo;
        private CarsService _sut;
        private Mock<IUnitOfWork> _unit;

        public CarServiceUT()
        {
            _carRepo = new Mock<ICarRepository>();
            _bodyRepo = new Mock<IBodyTypeRepository>();
            _unit = new Mock<IUnitOfWork>();
            _sut = new CarsService(_carRepo.Object, _bodyRepo.Object, _unit.Object);
            _carList = new List<CarDTOWithBodyType>
            {
                new CarDTOWithBodyType(),
                new CarDTOWithBodyType()
            };

            _carRepo.Setup(c => c.GetAll<CarDTOWithBodyType>()).Returns(_carList);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void AddCar_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            CarDTOWithBodyType carDto = new CarDTOWithBodyType()
            {
                Id = 1,
                Model = "Test",
                Maker = "Test",
                Year = 2005,
                BodyTypeId = 1
            };
            BodyTypeDTO dto = null;
            _bodyRepo.Setup(b => b.GetById(1)).Returns(dto);
            _sut.AddCar(carDto, messages);

            _carRepo.Verify(c => c.AddCar(carDto), Times.Never);
            _unit.Verify(u => u.SaveChanges(), Times.Never);
        }

        [Fact]
        public void AddCar_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            CarDTOWithBodyType carDto = new CarDTOWithBodyType()
            {
                Id = 1,
                Model = "Test",
                Maker = "Test",
                Year = 2005,
                BodyTypeId = 1
            };
            BodyTypeDTO dto = new BodyTypeDTO
            {
                Id = 1,
                TypeOfBody = "Test"
            };
            _bodyRepo.Setup(b => b.GetById(1)).Returns(dto);
            _sut.AddCar(carDto, messages);

            _carRepo.Verify(c => c.AddCar(carDto), Times.Once);
            _unit.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteCar_Invalid()
        {
            CarDTO car = null;
            _carRepo.Setup(c => c.GetCar<CarDTO>(It.IsAny<int>())).Returns(car);
            _sut.DeleteCar(1);
            _carRepo.Verify(c => c.DeleteCar(It.IsAny<int>()), Times.Never());
            _unit.Verify(u => u.SaveChanges(), Times.Never());
        }

        [Fact]
        public void DeleteCar_Valid()
        {
            CarDTO car = new CarDTO
            {
                Id = 1,
                Model = "Test",
                Maker = "Test",
                Year = 2005
            };
            _carRepo.Setup(c => c.GetCar<CarDTO>(It.IsAny<int>())).Returns(car);
            _sut.DeleteCar(1);
            _carRepo.Verify(c => c.DeleteCar(It.IsAny<int>()), Times.Once());
            _unit.Verify(u => u.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Edit_CarDTO_InvalidBodyType()
        {
            CarDTOWithBodyType carDto = new CarDTOWithBodyType()
            {
                Id = 1,
                Model = "Test",
                Maker = "Test",
                Year = 2005,
                BodyTypeId = 999
            };
            BodyTypeDTO dto = null;
            ValidationMessageList messages = new ValidationMessageList();
            _bodyRepo.Setup(b => b.GetById(It.IsAny<int>())).Returns(dto);
            _sut.Edit(1, carDto, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_BODYTYPE);
        }

        [Fact]
        public void Edit_CarDTO_InvalidCar()
        {
            CarDTOWithBodyType carDto = new CarDTOWithBodyType()
            {
                Id = 1,
                Model = "Test",
                Maker = "Test",
                Year = 2005,
                BodyTypeId = 1
            };
            BodyTypeDTO dto = new BodyTypeDTO
            {
                Id = 1,
                TypeOfBody = "Test"
            };
            ValidationMessageList messages = new ValidationMessageList();

            ValidationMessage error = new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR);

            _bodyRepo.Setup(b => b.GetById(It.IsAny<int>())).Returns(dto);

            _carRepo.Setup(c => c.Edit(carDto, messages))
                .Callback((CarDTOWithBodyType inCarDTO, ValidationMessageList inMessages) => inMessages.Add(error));

            _sut.Edit(1, carDto, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_CAR);
        }

        [Fact]
        public void Edit_CarDTO_InvalidID()
        {
            CarDTOWithBodyType carDto = new CarDTOWithBodyType()
            {
                Id = 5,
                Model = "Test",
                Maker = "Test",
                Year = 2005
            };
            ValidationMessageList messages = new ValidationMessageList();
            _sut.Edit(1, carDto, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.ID_NOT_MATCH);
        }

        [Fact]
        public void Edit_CarDTO_Valid()
        {
            CarDTOWithBodyType carDto = new CarDTOWithBodyType()
            {
                Id = 1,
                Model = "Test",
                Maker = "Test",
                Year = 2005,
                BodyTypeId = 1
            };
            BodyTypeDTO dto = new BodyTypeDTO
            {
                Id = 1,
                TypeOfBody = "Test"
            };
            ValidationMessageList messages = new ValidationMessageList();
            _bodyRepo.Setup(b => b.GetById(It.IsAny<int>())).Returns(dto);

            _sut.Edit(1, carDto, messages);

            _carRepo.Verify(c => c.Edit(carDto, messages), Times.Once());
            _unit.Verify(u => u.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetAll_BodyTypeDTO_Valid()
        {
            ICollection<BodyTypeDTO> _list = new List<BodyTypeDTO>
            {
                new BodyTypeDTO(),
                new BodyTypeDTO()
            };
            _bodyRepo.Setup(b => b.GetAll()).Returns(_list);
            ICollection<BodyTypeDTO> actual = _sut.GetAllBodyTypes();
            Assert.Equal(actual.Count, _carList.Count);
        }

        [Fact]
        public void GetAll_Valid()
        {
            _carRepo.Setup(c => c.GetAll<CarDTOWithBodyType>()).Returns(_carList);
            ICollection<CarDTOWithBodyType> actual = _sut.GetAll<CarDTOWithBodyType>();
            Assert.Equal(actual.Count, _carList.Count);
        }

        [Fact]
        public void GetBodyType_BodyTypeDTO_Valid()
        {
            BodyTypeDTO dto = new BodyTypeDTO
            {
                Id = 1,
                TypeOfBody = "Test"
            };

            _bodyRepo.Setup(b => b.GetById(It.IsAny<int>())).Returns(dto);
            BodyTypeDTO actual = _sut.GetBodyTypeById(1);
            Assert.Equal(actual.Id, dto.Id);
        }

        [Fact]
        public void GetCar_CarDTO_Invalid()
        {
            CarDTOWithBodyType dto = null;

            ValidationMessageList messages = new ValidationMessageList();
            _carRepo.Setup(c => c.GetCar<CarDTOWithBodyType>(It.IsAny<int>())).Returns(dto);
            CarDTOWithBodyType actual = _sut.GetCar(1, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_CAR);
        }

        [Fact]
        public void GetCar_CarDTO_Valid()
        {
            CarDTOWithBodyType dto = new CarDTOWithBodyType
            {
                Id = 1,
                Maker = "Test",
                Model = "Test",
                Year = 2010
            };

            ValidationMessageList messages = new ValidationMessageList();
            _carRepo.Setup(c => c.GetCar<CarDTOWithBodyType>(It.IsAny<int>())).Returns(dto);
            CarDTOWithBodyType actual = _sut.GetCar(1, messages);
            Assert.Equal(actual.Id, dto.Id);
        }
    }
}
