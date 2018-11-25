using FluentAssertions;
using kata.LawnMower;
using kata.LawnMower.Infrastructure;
using kata.LawnMower.Models;
using kata.LawnMower.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class LawnMowerControllerShould
    {
        private LawnMowerController _sut;


        [SetUp]
        public void Setup()
        {
            var positionMock = new Mock<IPosition>();
            positionMock.Setup(x => x.Orientation).Returns('N');
            positionMock.Setup(x => x.XAxis).Returns(5);
            positionMock.Setup(x => x.YAxis).Returns(5);

            var gardenSizeMock = new Mock<ISize>();
            gardenSizeMock.Setup(x => x.Lenght).Returns(10);
            gardenSizeMock.Setup(x => x.Width).Returns(10);

            var settingsMock = new Mock<ISettings>();
            settingsMock.Setup(x => x.DevicePosition).Returns(positionMock.Object);
            settingsMock.Setup(x => x.GardenSize).Returns(gardenSizeMock.Object);

            var buffer = new DeviceBuffer();
            var actuator = new TestsActuator(true);
            var device = new SLMMDevice(settingsMock.Object, buffer, actuator);

            _sut = new LawnMowerController(device);
        }

        [Test]
        public async Task return_ok_for_current_position()
        {
            // act
            var result = await _sut.GetCurrentPosition();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var position = okResult.Value.Should().BeAssignableTo<IPosition>().Subject;

            position.Orientation.Should().Be('N');
            position.XAxis.Should().Be(5);
            position.YAxis.Should().Be(5);

        }

        
    }
}