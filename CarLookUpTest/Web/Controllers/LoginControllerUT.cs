using CarLookUp.Core.ApplicationSettings;
using CarLookUp.Core.Constants;
using CarLookUp.Core.Enum;
using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.Services.Interfaces;
using CarLookUp.UnitTest.Helpers;
using CarLookUp.Web.Controllers;
using CarLookUp.Web.Mappers;
using CarLookUp.Web.ViewModels;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace CarLookUp.UnitTest.Web.Controllers
{
    public class LoginControllerUT : HttpContextHelper
    {
        private ICollection<RoleDTO> _list;
        private Mock<ILoginService> _loginService;
        private Mock<IRoleService> _roleService;
        private LoginController _sut;

        public LoginControllerUT()
        {
            _roleService = new Mock<IRoleService>();
            _loginService = new Mock<ILoginService>();
            _sut = new LoginController(_roleService.Object, _loginService.Object);
            _list = new List<RoleDTO>
            {
                new RoleDTO(),
                new RoleDTO()
            };
            _roleService.Setup(s => s.GetAll()).Returns(_list);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Index_Valid()
        {
            ViewResult actual = _sut.Index() as ViewResult;
            Assert.Equal("", actual.ViewName);
        }

        [Fact]
        public void Login_Invalid_ErrorMessages()
        {
            UserVM user = new UserVM
            {
                UserName = "Test",
                RoleId = 1
            };
            UserDTO userDto = new UserDTO()
            {
                UserName = "TEST",
                Role = new RoleDTO()
            };
            ValidationMessageList messages = new ValidationMessageList();

            _loginService.Setup(l => l.LoginUser(It.IsAny<UserDTO>(), messages))
                .Callback((UserDTO u, ValidationMessageList l) => l.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_ROLE)));

            _sut.Login(user);

            RedirectToRouteResult actual = _sut.Login(user) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Login_Invalid_ModelState()
        {
            UserVM user = new UserVM
            {
                UserName = "Test",
                RoleId = 1
            };
            _sut.ModelState.AddModelError("error", "test error");

            RedirectToRouteResult actual = _sut.Login(user) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Login_Valid()
        {
            UserVM user = new UserVM
            {
                UserName = "Test",
                RoleId = 1
            };
            UserDTO userDto = new UserDTO()
            {
                UserName = "TEST",
                Role = new RoleDTO()
            };
            _sut.Login(user);

            RedirectToRouteResult actual = _sut.Login(user) as RedirectToRouteResult;
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void Logoff_Valid()
        {
            RedirectToRouteResult actual = _sut.Logoff() as RedirectToRouteResult;
            _loginService.Verify(s => s.Logoff(), Times.Once);
            Assert.True(actual.RouteValues.ContainsValue("Index"));
        }
    }
}
