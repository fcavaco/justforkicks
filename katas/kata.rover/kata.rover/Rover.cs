using System;
using System.Collections.Generic;

namespace kata.rover
{
    public class Grid{
        public Grid(int width, int height){
            _width = width;
            _height = height;
        }
        private readonly int _width = 10;
        private readonly int _height = 10;

        public int Width => _width;
        public int Height => _height;
    }
    public class Rover    
    {
        private int[] _position = new int[]{0,0};
        private readonly Grid _grid;

        public static char MOVE { get => 'M'; }

        private Coordinate _coordinates;
        private Direction _direction;
        public Rover(){
            _grid = new Grid(10,10);
            _direction = new Direction(CardinalPoint.NORTH);
            _coordinates = new Coordinate(0,0);
        }
        public Rover(Grid grid){
            _grid = grid;
            _direction = new Direction(CardinalPoint.NORTH);
            _coordinates = new Coordinate(0,0);
        }

        public string Execute(string commands)
        {
            foreach (char command in commands.ToCharArray())
            {
                if (command == Direction.RIGHT)
                {
                    _direction.TurnRight();
                }

                if (command == Direction.LEFT)
                {
                    _direction.TurnLeft();
                }

                if (command == Rover.MOVE)
                {
                    _coordinates = this.Move(_direction);
                }
            }
            return Position();
        }

            private Coordinate Move(Direction direction)
            {
            
            int y = _coordinates.Y;
            int x = _coordinates.X;

            if(direction.Value == CardinalPoint.NORTH){
                y = (y + _grid.Height + 1) % _grid.Height;
            }
           if(direction.Value == CardinalPoint.SOUTH){
                y = (y + _grid.Height - 1) % _grid.Height;
            }

            if(direction.Value == CardinalPoint.EAST){
                x++;
            }

            return new Coordinate(x,y);
        }

        private string Position()
        {
            return $"{_coordinates.ToString()}:{_direction.Value}";
        }
    }
}
