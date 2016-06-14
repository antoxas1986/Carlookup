using CarLookUp.Core.Constants;
using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.Services;
using CarLookUp.Services.Interfaces;
using CarLookUp.UnitTest.Helpers;
using CarLookUp.Web.Mappers;
using Moq;
using Xunit;

namespace CarLookUp.UnitTest.Services
{
    public class LoginServiceUT : HttpContextHelper
    {
        private Mock<IRoleService> _roleService;
        private LoginService _sut;

        public LoginServiceUT()
        {
            _roleService = new Mock<IRoleService>();
            _sut = new LoginService(_roleService.Object);
            AutoMapperConfig.Execute();
        }

        [Fact]
        public void LoginUser_Invalid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            RoleDTO roleDto = null;
            UserDTO userDro = new UserDTO
            {
                UserName = "Test",
                Role = new RoleDTO
                {
                    Id = 1
                }
            };
            _roleService.Setup(r => r.GetById(It.IsAny<int>())).Returns(roleDto);
            _sut.LoginUser(userDro, messages);
            Assert.Equal(messages.GetFirstErrorMsg, ErrorMessages.NO_ROLE);
        }

        [Fact]
        public void LoginUser_Valid()
        {
            ValidationMessageList messages = new ValidationMessageList();
            RoleDTO roleDto = new RoleDTO
            {
                Id = 1,
                Name = "Test"
            };
            UserDTO userDto = new UserDTO
            {
                UserName = "Test",
                Role = new RoleDTO
                {
                    Id = 1
                }
            };
            SessionManager.User = userDto;
            _roleService.Setup(r => r.GetById(It.IsAny<int>())).Returns(roleDto);
            _sut.LoginUser(userDto, messages);

            string expected = userDto.UserName;
            string actual = SessionManager.User.UserName;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Logoff_Invalid()
        {
            SessionManager.User = null;
            _sut.Logoff();
            Assert.Null(SessionManager.User);
        }
    }
}
