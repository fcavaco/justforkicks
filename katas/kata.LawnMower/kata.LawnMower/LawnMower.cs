using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using kata.LawnMower.Infrastructure;
using kata.LawnMower.Models;

namespace kata.LawnMower
{
    public interface IActuator
    {
        void Turn(bool clockwise);
        void Move(int units);
    }
    public class TestsActuator : IActuator
    {
        private readonly bool _stubTimePassed;
        private int TIME_PASSED_TURNING=2000;
        private int TIME_PASSED_MOVING=5000;
        public TestsActuator(bool stubTimePassed)
        {
            _stubTimePassed = stubTimePassed;
        }
        public void Move(int units)
        {
            if (_stubTimePassed)
            {
                // simulate actual call to device mechanics
                Thread.Sleep(TIME_PASSED_MOVING * units);
            }
            
        }

        public void Turn(bool clockwise)
        {
            if (_stubTimePassed)
            {
                // simulate actual call to device mechanics
                Thread.Sleep(TIME_PASSED_TURNING); 
            }
        }
    }
    public class SLMMDevice
    {
        private readonly ISettings _settings;
        private IPosition _currentPosition;
        private readonly IBuffer _buffer;
        private readonly IActuator _actuator;

        public SLMMDevice(ISettings settings, IBuffer buffer, IActuator actuator)
        {
            _settings = settings;
            _buffer = buffer;
            
            _buffer.Add(_settings.DevicePosition);
            _actuator = actuator;
        }

        
        public IPosition CurrentPosition()
        {
            return _buffer.Peek().Result;
        }
        
        public async Task<IPosition> TurnClockwise()
        {
            var position = await _buffer.Pop();

            var newOrientation = NewClockwiseOrientation(position.Orientation);

            var newPosition = NewPosition(position, newOrientation);

            _buffer.Add(newPosition);

            _actuator.Turn(true);

            return newPosition;
        }

        public async Task<IPosition> Move()
        {
            var oldPos = this.CurrentPosition();

            var newPos = oldPos.Move(_settings.GardenSize);

            _buffer.Add(newPos);

            _actuator.Move(1);

            return newPos;
        }

        public async Task<IPosition> TurnAntiClockwise()
        {
            var position = await _buffer.Pop();

            var newOrientation = NewAntiClockwiseOrientation(position.Orientation);

            var newPosition = NewPosition(position, newOrientation);

            _buffer.Add(newPosition);

            _actuator.Turn(false);

            return newPosition;
        }

        private char NewClockwiseOrientation(char current)
        {
            var result = current;

            switch (current)
            {
                case Oriented.NORTH:
                    result = Oriented.EAST;
                    break;
                case Oriented.EAST:
                    result = Oriented.SOUTH;
                    break;
                case Oriented.SOUTH:
                    result = Oriented.WEST;
                    break;
                case Oriented.WEST:
                    result = Oriented.NORTH;
                    break;
            }

            return result;

        }

        private char NewAntiClockwiseOrientation(char current)
        {
            var result = current;

            switch (current)
            {
                case Oriented.NORTH:
                    result = Oriented.WEST;
                    break;
                case Oriented.WEST:
                    result = Oriented.SOUTH;
                    break;
                case Oriented.SOUTH:
                    result = Oriented.EAST;
                    break;
                case Oriented.EAST:
                    result = Oriented.NORTH;
                    break;
            }

            return result;

        }

        
        private IPosition NewPosition(IPosition old, char orientation)
        {
            return new DevicePosition(old.YAxis, old.XAxis, orientation);
        }

       
    }
}
