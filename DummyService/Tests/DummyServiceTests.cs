using Xunit;
using aspnet.Services;

namespace aspnet.UnitTests.Services
{
    public class DummyService_IsDummyShould
    {
        [Fact]
        public void IsDummy_InputIs1_ReturnFalse()
        {
            var primeService = new DummyService();
            bool result = primeService.IsDummy(1);

            Assert.False(result, "1 should not be dummy");
        }
    }
}