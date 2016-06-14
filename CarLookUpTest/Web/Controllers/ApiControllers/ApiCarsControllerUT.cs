using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.Controllers.ApiControllers;
using CarLookUp.Web.Mappers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace CarLookUp.UnitTest.Web.Controllers.ApiControllers
{
    public class ApiCarsControllerUT
    {
        private Mock<ICarsService> _carService;
        private List<CarDTO> _list;
        private CarsController _sut;

        public ApiCarsControllerUT()
        {
            _carService = new Mock<ICarsService>();

            _sut = new CarsController(_carService.Object);
            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new System.Web.Http.HttpConfiguration();
            _list = new List<CarDTO>
            {
                new CarDTO(),
                new CarDTO()
            };
            _carService.Setup(c => c.GetAll<CarDTO>()).Returns(_list);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Delete_Valid()
        {
            var response = _sut.Delete(It.IsAny<int>());

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public void Get_Valid()
        {
            var response = _sut.Get();
            Assert.Equal(response.Count(), _list.Count);
        }
    }
}
