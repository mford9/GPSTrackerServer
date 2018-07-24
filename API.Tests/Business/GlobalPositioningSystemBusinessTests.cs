using AutoFixture;
using Ford.Tracker.Api.Business;
using Ford.Tracker.Api.Messaging.Payload;
using Moq;
using Rebus.Bus;
using Rebus.Testing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Business
{
    public class GlobalPositioningSystemBusinessTests 
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public async Task MapAndSendToMessengerAsync_WhenCalledOnce_CallSendOnBus()
        {
            // Arrange
            var bus = new FakeBus();
            var gpsData = _fixture.Create<string>();
            
            // Act
            var business = GetBusiness(bus);

            await business.MapAndSendToMessengerAsync(gpsData);
            
            // Assert
            Assert.Single(bus.Events.ToList());
        }

        [Fact]
        public async Task MapAndSendToMessengerAsync_WhenCalledManyTimes_CallSendOnBus()
        {
            // Arrange
            var bus = new FakeBus();
            var gpsDataOne = _fixture.Create<string>();
            var gpsDataTwo = _fixture.Create<string>();

            // Act
            var business = GetBusiness(bus);

            var taskOne = business.MapAndSendToMessengerAsync(gpsDataOne);
            var taskTwo = business.MapAndSendToMessengerAsync(gpsDataTwo);

            await taskOne;
            await taskTwo;

            // Assert
            Assert.Equal(2, bus.Events.ToList().Count);
        }

        private IGlobalPositioningSystemBusiness GetBusiness(IBus bus = null)
        {
            if (bus == null)
            {
                bus = new FakeBus();
            }

            return new GlobalPositioningSystemBusiness(bus);
        }
    }
}
