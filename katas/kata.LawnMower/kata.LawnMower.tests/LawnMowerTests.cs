using Microsoft.Extensions.Configuration;
using kata.LawnMower;
using kata.LawnMower.Infrastructure;
using kata.LawnMower.Models;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading;

namespace Tests
{
    public class SLMMDeviceShould
    {
        private Mock<ISettings> _initialSettingsMock;
        private const int GARDENSIZE_LENGHT = 10;
        private const int GARDENSIZE_WIDTH = 10;
        private const int DEVICE_POSITION_Y = 5;
        private const int DEVICE_POSITION_X = 4;
        private const char DEVICE_ORIENTATION = 'N';

        private SLMMDevice _device;
        private IBuffer _buffer;
        private IActuator _actuator;

        [SetUp]
        public void Setup()
        {
            _initialSettingsMock = SetInitialSettingsMock(
                    GARDENSIZE_LENGHT, 
                    GARDENSIZE_WIDTH, 
                    DEVICE_POSITION_Y,
                    DEVICE_POSITION_X,
                    DEVICE_ORIENTATION);

            _buffer = new DeviceBuffer();
            _actuator = new TestsActuator(false);
            _device = new SLMMDevice(_initialSettingsMock.Object,_buffer, _actuator);
        }

        [Test]
        public void present_initial_position_from_configuration_settings()
        {
            // arrange 
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer,_actuator);

            // act
            var position = _device.CurrentPosition();

            // assert
            Assert.AreEqual(DEVICE_POSITION_X, position.XAxis);
            Assert.AreEqual(DEVICE_POSITION_Y, position.YAxis);
            Assert.AreEqual(DEVICE_ORIENTATION, position.Orientation);

        }

        [Test]
        [TestCase('N','E')]
        [TestCase('E','S')]
        [TestCase('S','W')]
        [TestCase('W','N')]
        public async Task be_able_turn_90_degrees_clockwise(char currentOrientation, char expectedOrientation)
        {
            // arrange

            IPosition setting = new DevicePosition(5, 5, currentOrientation) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer,_actuator);

            // act
            var position = await _device.TurnClockwise();
            
            // assert
            Assert.AreEqual(expectedOrientation, position.Orientation);
        }

        [Test]
        public async Task turns_clockwise_in_around_2_seconds()
        {
            var actuator = new TestsActuator(true);
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, actuator);

            // act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var position = await _device.TurnClockwise();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            // assert
            Assert.GreaterOrEqual(elapsedMs,2000);
            Assert.LessOrEqual(elapsedMs, 3000);
        }

        [Test]
        public async Task turns_anti_clockwise_in_around_2_seconds()
        {
            var actuator = new TestsActuator(true);
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, actuator);

            // act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var position = await _device.TurnCounterClockwise();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            // assert
            Assert.GreaterOrEqual(elapsedMs, 2000);
            Assert.LessOrEqual(elapsedMs, 3000);
        }
        [Test]
        [TestCase('E',3,'N')]
        [TestCase('E', 5, 'S')]
        public async Task be_able_turn_clockwise_in_sequence(char start, int numberTurns, char expected)
        {
            // arrange
            IPosition setting = new DevicePosition(5, 5, start) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer,_actuator);

            IPosition position=_device.CurrentPosition();

            // act
            for(int i = 0; i < numberTurns; i++)
            {
                position = await _device.TurnClockwise();
            }

            // assert
            Assert.AreEqual(expected, position.Orientation);
        }

        [Test]
        [TestCase('W', 3, 'N')]
        [TestCase('E', 5, 'N')]
        public async Task be_able_turn_anti_clockwise_in_sequence(char start, int numberTurns, char expected)
        {
            // arrange
            IPosition setting = new DevicePosition(5, 5, start) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer,_actuator);

            IPosition position = _device.CurrentPosition();

            // act
            for (int i = 0; i < numberTurns; i++)
            {
                position = await _device.TurnCounterClockwise();
            }

            // assert
            Assert.AreEqual(expected, position.Orientation);
        }
        [Test]
        [TestCase('N', 'W')]
        [TestCase('W', 'S')]
        [TestCase('S', 'E')]
        [TestCase('E', 'N')]
        public async Task be_able_turn_90_degrees_anti_clockwise(char currentOrientation, char expectedOrientation)
        {
            // arrange

            IPosition setting = new DevicePosition(5, 5, currentOrientation) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            var _device = new SLMMDevice(_initialSettingsMock.Object, _buffer,_actuator);

            // act
            var position = await _device.TurnCounterClockwise();

            // assert
            Assert.AreEqual(expectedOrientation, position.Orientation);
        }

        [Test]
        [TestCase('N',6,5)]
        [TestCase('E', 5, 6)]
        [TestCase('S', 4, 5)]
        [TestCase('W', 5, 4)]
        public async Task move_one_step_forward(char orientation, int expectedY, int expectedX )
        {
            // garden: 0 x 0 -> 10 x 10 
            // starts at: 'N', y:5, x:5
            // expects: 'N', y + 1 = 6, x

            IPosition setting = new DevicePosition(5, 5, orientation) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, _actuator);

            var position = _device.CurrentPosition();

            IPosition newPosition = await _device.Move();

            Assert.AreEqual(expectedY, newPosition.YAxis);
            Assert.AreEqual(expectedX, newPosition.XAxis);
            Assert.AreEqual(orientation, newPosition.Orientation);
        }

        [Test]
        [TestCase('N', 7, 5)]
        [TestCase('E', 5, 7)]
        [TestCase('S', 3, 5)]
        [TestCase('W', 5, 3)]
        public async Task move_two_step_forward(char orientation, int expectedY, int expectedX)
        {
            // garden: 0 x 0 -> 10 x 10 
            // starts at: 'N', y:5, x:5
            // expects: 'N', y + 1 = 6, x

            IPosition setting = new DevicePosition(5, 5, orientation) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, _actuator);

            var position = _device.CurrentPosition();

            IPosition newPosition = await _device.Move();
            newPosition = await _device.Move();

            Assert.AreEqual(expectedY, newPosition.YAxis);
            Assert.AreEqual(expectedX, newPosition.XAxis);
            Assert.AreEqual(orientation, newPosition.Orientation);
        }

        [Test]
        [TestCase('N',8,5)]
        [TestCase('E', 5, 8)]
        [TestCase('S', 2, 5)]
        [TestCase('W', 5, 2)]
        public async Task move_in_sequence_and_end_at_expected_position(char orientation, int expectedY, int expectedX)
        {
            IPosition setting = new DevicePosition(5, 5, orientation) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, _actuator);

            var position = _device.CurrentPosition();

            _device.Move();
            _device.Move();
            IPosition newPosition = await _device.Move();

            Assert.AreEqual(expectedY, newPosition.YAxis);
            Assert.AreEqual(expectedX, newPosition.XAxis);
            Assert.AreEqual(orientation, newPosition.Orientation);
        }

        [Test]
        [TestCase('N', 9, 5)]
        [TestCase('E', 5, 9)]
        [TestCase('S', 0, 5)]
        [TestCase('W', 5, 0)]
        public async Task move_should_stop_at_the_edge_of_the_garden(char orientation, int expectedY, int expectedX)
        {
            IPosition setting = new DevicePosition(5, 5, orientation) as IPosition;
            _initialSettingsMock.Setup(x => x.DevicePosition).Returns(setting);
            _buffer = new DeviceBuffer();
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, _actuator);
            
            var position = _device.CurrentPosition();

            _device.Move();
            _device.Move();
            _device.Move();
            _device.Move();
            _device.Move();
            _device.Move();
            _device.Move();
            _device.Move();
            _device.Move();
            IPosition newPosition = await _device.Move();

            Assert.AreEqual(expectedY, newPosition.YAxis);
            Assert.AreEqual(expectedX, newPosition.XAxis);
            Assert.AreEqual(orientation, newPosition.Orientation);
        }

        [Test]
        public async Task move_in_around_5_seconds()
        {
            var actuator = new TestsActuator(true);
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, actuator);

            // act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var position = await _device.Move();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            // assert
            Assert.GreaterOrEqual(elapsedMs, 5000);
            Assert.LessOrEqual(elapsedMs, 6000);
        }

        [Test]
        public async Task receive_move_instructions_and_buffer_them_for_execution()
        {
            var actuator = new TestsActuator(true);
            _device = new SLMMDevice(_initialSettingsMock.Object, _buffer, actuator);

            // act
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
                _device.Move();
                _device.Move();
                _device.Move();
                var position = await _device.Move();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            // assert
            Assert.GreaterOrEqual(elapsedMs, 5000 * 4);
            Assert.LessOrEqual(elapsedMs, 6000 * 4);
        }

        #region private helper methods
        private static Mock<ISettings> SetInitialSettingsMock(int gardenLenght, int gardenWidth, int yAxis, int xAxis, char orientation)
        {
            var configurationMock = new Mock<IConfiguration>();
            var settings = new Mock<ISettings>();

            var gardenSize = new GardenSize(gardenLenght, gardenWidth);
            settings.Setup(x => x.GardenSize).Returns(gardenSize as ISize);

            var devicePosition = new DevicePosition(yAxis, xAxis, orientation);
            settings.SetupGet(x => x.DevicePosition).Returns(devicePosition as IPosition);
            return settings;
        }

        #endregion private helper methods
    }
}