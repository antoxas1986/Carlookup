using CarLookUp.Core.Models;
using CarLookUp.Core.Utilities;
using CarLookUp.UnitTest.Helpers;
using Xunit;

namespace CarLookUp.UnitTest
{
    public class Class1 : HttpContextHelper
    {
        private bool _bool;
        private UserDTO _user;

        public Class1()
        {
            _bool = true;
            _user = new UserDTO
            {
                UserName = "Test"
            };
        }

        [Fact]
        public void Session_Valid()
        {
            SessionManager.User = _user;

            string expected = _user.UserName;
            string actual = SessionManager.User.UserName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test2()
        {
            Assert.True(_bool);
        }
    }
}
