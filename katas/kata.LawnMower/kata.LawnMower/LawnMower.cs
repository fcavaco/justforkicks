using System;
using System.Collections.Generic;
using System.Linq;
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

    public interface IControlledDevice
    {
        IPosition CurrentPosition();
        Task<IPosition> TurnClockwise();
        Task<IPosition> TurnCounterClockwise();
        Task<IPosition> Move();
    }
    public class SLMMDevice:IControlledDevice
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

            var newOrientation = OrientationClockwiseTurn(position.Orientation);

            var newPosition = position.Turn(newOrientation);

            _buffer.Add(newPosition);

            _actuator.Turn(true);

            return newPosition;
        }

        public async Task<IPosition> Move()
        {
            var oldPos = await _buffer.Pop();

            var newPos = oldPos.Move(_settings.GardenSize);

            _buffer.Add(newPos);

            _actuator.Move(1);

            return newPos;
        }

        public async Task<IPosition> TurnCounterClockwise()
        {
            var position = await _buffer.Pop();

            var newOrientation = OrientationCounterClockwiseTurn(position.Orientation);

            var newPosition = position.Turn(newOrientation);

            _buffer.Add(newPosition);

            _actuator.Turn(false);

            return newPosition;
        }

        private IList<Tuple<char, int>> cardinals = new List<Tuple<char, int>>()
        {
            new Tuple<char, int>(Oriented.EAST,-3),
            new Tuple<char, int>(Oriented.SOUTH,-2),
            new Tuple<char, int>(Oriented.WEST,-1),
            new Tuple<char, int>(Oriented.NORTH,0),
            new Tuple<char, int>(Oriented.EAST,1),
            new Tuple<char, int>(Oriented.SOUTH,2),
            new Tuple<char, int>(Oriented.WEST,3)
        };

        private char OrientationClockwiseTurn(char currentOrientation)
        {
            var item = cardinals.First(x => x.Item1.Equals(currentOrientation));
            var newItemIndex = (item.Item2 + 1) % 4;
            var newOrientation = cardinals.Single(x => x.Item2 == newItemIndex).Item1;
            return newOrientation;
        }

        private char OrientationCounterClockwiseTurn(char currentOrientation)
        {
            var item = cardinals.First(x => x.Item1.Equals(currentOrientation));
            var newItemIndex = (item.Item2 - 1) % 4;
            var newOrientation = cardinals.Single(x => x.Item2 == newItemIndex).Item1;
            return newOrientation;
        }
       
    }
}
