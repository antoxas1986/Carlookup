using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.Controllers;
using CarLookUp.Web.Mappers;
using CarLookUp.Web.ViewModels;
using Moq;
using Postal;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace CarLookUp.UnitTest.Web.Controllers
{
    public class CarsControllerUT
    {
        private Mock<ICarsService> _carService;
        private Mock<IEmailService> _emailService;
        private ICollection<BodyTypeDTO> _list;
        private CarsController _sut;

        public CarsControllerUT()
        {
            _carService = new Mock<ICarsService>();
            _emailService = new Mock<IEmailService>();
            _sut = new CarsController(_carService.Object, _emailService.Object);
            _list = new List<BodyTypeDTO>
            {
                new BodyTypeDTO(),
                new BodyTypeDTO()
            };
            _carService.Setup(s => s.GetAllBodyTypes()).Returns(_list);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Create_Invalid_HasError_Post()
        {
            _carService.Setup(c => c.AddCar(It.IsAny<CarDTOWithBodyType>(), It.IsAny<ValidationMessageList>()))
                .Callback((CarDTOWithBodyType inCar, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR)));
            ViewResult actual = _sut.Create(It.IsAny<CarVMWithBodyTypeName>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
                .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_CAR);
        }

        [Fact]
        public void Create_Invalid_ModelState_Post()
        {
            _sut.ModelState.AddModelError("error", "test error");

            ViewResult actual = _sut.Create(It.IsAny<CarVMWithBodyTypeName>()) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Create_Valid()
        {
            ViewResult actual = _sut.Create() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Create_Valid_Post()
        {
            CarVMWithBodyTypeName car = new CarVMWithBodyTypeName()
            {
                Id = 1,
                Maker = "Test",
                Model = "Test",
                Year = 2010
            };
            RedirectToRouteResult actual = _sut.Create(car) as RedirectToRouteResult;
            _emailService.Verify(e => e.Send(It.IsAny<DetailsEmailVM>()), Times.Once);
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Delete_Invalid_Get()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR)));

            ViewResult actual = _sut.Delete(1) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
                .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_CAR);
        }

        [Fact]
        public void Delete_Invalid_Post()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR)));

            ViewResult actual = _sut.Delete(1, new CarVM()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
                .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_CAR);
        }

        [Fact]
        public void Delete_Valid_Get()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new CarDTOWithBodyType());
            ViewResult actual = _sut.Delete(1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Delete_Valid_Post()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new CarDTOWithBodyType());
            RedirectToRouteResult actual = _sut.Delete(1, new CarVM()) as RedirectToRouteResult;
            _carService.Verify(c => c.DeleteCar(It.IsAny<int>()), Times.Once);
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Details_Invalid()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR)));

            HttpNotFoundResult actual = _sut.Details(1) as HttpNotFoundResult;

            Assert.Equal("404", actual.StatusCode.ToString());
        }

        [Fact]
        public void Details_Valid()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new CarDTOWithBodyType());
            ViewResult actual = _sut.Details(1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Edit_Invalid_Get()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR)));

            HttpNotFoundResult actual = _sut.Edit(1) as HttpNotFoundResult;

            Assert.Equal("404", actual.StatusCode.ToString());
        }

        [Fact]
        public void Edit_Invalid_HasError_Post()
        {
            _carService.Setup(c => c.Edit(It.IsAny<int>(), It.IsAny<CarDTOWithBodyType>(), It.IsAny<ValidationMessageList>()))
                .Callback((int inId, CarDTOWithBodyType inCar, ValidationMessageList inMessages) => inMessages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_CAR)));

            ViewResult actual = _sut.Edit(1, It.IsAny<CarVMWithBodyTypeName>()) as ViewResult;
            string error = actual.ViewData.ModelState.Where(m => m.Key == string.Empty)
               .Select(m => m.Value).FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            Assert.Equal("", actual.ViewName);
            Assert.Equal(error, ErrorMessages.NO_CAR);
        }

        [Fact]
        public void Edit_Invalid_ModelState_Post()
        {
            _sut.ModelState.AddModelError("error", "test error");

            ViewResult actual = _sut.Edit(1, It.IsAny<CarVMWithBodyTypeName>()) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Edit_Valid_Get()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new CarDTOWithBodyType());
            ViewResult actual = _sut.Edit(1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Edit_Valid_Post()
        {
            _carService.Setup(c => c.GetCar(It.IsAny<int>(), It.IsAny<ValidationMessageList>())).Returns(new CarDTOWithBodyType());
            RedirectToRouteResult actual = _sut.Edit(1, It.IsAny<CarVMWithBodyTypeName>()) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Index_Valid()
        {
            ViewResult actual = _sut.Index() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }
    }
}
