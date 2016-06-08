using Xunit;

namespace CarLookUp.UnitTest
{
    public class Class1
    {
        private bool _bool;

        public Class1()
        {
            _bool = true;
        }

        [Fact]
        public void Test2()
        {
            Assert.True(_bool);
        }
    }
}
