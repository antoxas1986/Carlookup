using CarLookUp.Controllers;
using CarLookUp.Core.Models;
using CarLookUp.Services.Interfaces;
using CarLookUp.Web.Mappers;
using Moq;
using System;
using System.Web.Mvc;
using Xunit;

namespace CarLookUp.UnitTest.Web.Controllers
{
    public class RoleControllerUT
    {
        private Mock<IRoleService> _roleService;
        private RoleController _sut;

        public RoleControllerUT()
        {
            _roleService = new Mock<IRoleService>();
            _sut = new RoleController(_roleService.Object);
            AutoMapperConfig.Execute();
        }

        //[Fact]
        //public void Create_Valid_Exception()
        //{
        //    //Exception ex = Assert.Throws<Exception>(() => _sut.Create());
        //    //Assert.Equal("another exception", ex.Message);
        //}

        [Fact]
        public void Details_Invalid_Null_Role()
        {
            RoleDTO dto = null;
            _roleService.Setup(r => r.GetById(It.IsAny<int>())).Returns(dto);
            RedirectToRouteResult actual = _sut.Details(1) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Details_Valid()
        {
            RoleDTO role = new RoleDTO
            {
                Id = 1,
                Name = "Test"
            };
            _roleService.Setup(r => r.GetById(1)).Returns(role);
            ViewResult actual = _sut.Details(1) as ViewResult;
            Assert.Equal("", actual.ViewName);
        }
    }
}
