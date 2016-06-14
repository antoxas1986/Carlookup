using CarLookUp.Core.ApplicationSettings;
using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.UnitTest.Helpers;
using CarLookUp.Web.Controllers;
using CarLookUp.Web.Mappers;
using System.Web.Mvc;
using Xunit;

namespace CarLookUp.UnitTest.Web.Controllers
{
    public class HomeControllerUT : HttpContextHelper
    {
        private HomeController _sut;

        public HomeControllerUT()
        {
            _sut = new HomeController();
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void Index_Valid()
        {
            UserDTO user = new UserDTO
            {
                UserName = "TEST",
                Role = new RoleDTO
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            SessionManager.User = user;
            ViewResult actual = _sut.Index() as ViewResult;
            Assert.Equal("", actual.ViewName);
            Assert.Equal(user.UserName, SessionManager.User.UserName);
            Assert.Equal(actual.ViewBag.User.UserName, user.UserName);
            Assert.Equal(actual.ViewBag.Title, TestApplicationSettings.Test);
        }
    }
}
