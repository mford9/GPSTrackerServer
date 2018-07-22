using AutoFixture;
using Ford.Tracker.Api.Business;
using Ford.Tracker.Api.Controllers;
using Ford.Tracker.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Controller
{
    public class GlobalPositioningSystemControllerTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void CanConstruct()
        {
            // Act           
            var controller = GetController();

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task SendCurrentCoordinates_WhenCalled_CanReturnAcceptedHttpStatus()
        {
            // Arrange
            var gpsDto = new GlobalPositioningSystemCreateRequest();

            // Act
            var controller = GetController();

            var response = await controller.SendCurrentCoordinates(gpsDto);

            // Assert
            Assert.IsType<AcceptedResult>(response);
        }

        [Fact]
        public async Task SendCurrentCoordinated_WhenCalledWithNullParameter_ReturnsBadRequest()
        {
            // Arrange
            GlobalPositioningSystemCreateRequest gpsRequest = null;

            // Act
            var controller = GetController();

            var response = await controller.SendCurrentCoordinates(gpsRequest);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task SendCurrentCoordinated_WhenCalled_VerifyBusinessCallMapAndSendToMessenger()
        {
            // Arrange 
            var gpsPositiningData = _fixture.Create<string>();

            var request = new GlobalPositioningSystemCreateRequest
            {
                GlobalPositioningData = gpsPositiningData
            };

            var globalPositioningSystemBusiness = new Mock<IGlobalPositioningSystemBusiness>();
            
            // Act
            var controller = GetController(globalPositioningSystemBusiness);

            await controller.SendCurrentCoordinates(request);

            // Assert
            globalPositioningSystemBusiness.Verify(x => x.MapAndSendToMessengerAsync(gpsPositiningData), Times.Once);
        }


        private GlobalPositioningSystemController GetController(Mock<IGlobalPositioningSystemBusiness> globalPositioningSystemBusiness = null)
        {
            if (globalPositioningSystemBusiness == null)
            {
                globalPositioningSystemBusiness = new Mock<IGlobalPositioningSystemBusiness>();
            }

            return new GlobalPositioningSystemController(globalPositioningSystemBusiness.Object);
        }
    }
}
