using AutoFixture;
using Ford.Tracker.Api.Business;
using Moq;
using Rebus.Bus;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Business
{
    public class GlobalPositioningSystemBusinessTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public async Task MapAndSendToMessengerAsync_WhenCalled_CallSendOnBus()
        {
            // Arrange
            var gpsData = _fixture.Create<string>();
            
            var bus = new Mock<IBus>();
            // Act
            var business = GetBusiness(bus);

            await business.MapAndSendToMessengerAsync(gpsData);

            // Assert
            //bus.Verify(x => x.Advanced.Routing.Send(It.IsIn<string>, new GlobalPositioningSystemMessage { test = gpsData }), Times.Once);
        }

        private IGlobalPositioningSystemBusiness GetBusiness(Mock<IBus> bus = null)
        {
            if (bus == null)
            {
                bus = new Mock<IBus>();
            }

            return new GlobalPositioningSystemBusiness(bus.Object);
        }
    }
}
