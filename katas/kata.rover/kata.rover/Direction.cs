namespace kata.rover
{
    public class Direction{
      
        int _direction = 0;

        public static char RIGHT { get => 'R'; }
        public static char LEFT { get => 'L'; }

        char _value = CardinalPoint.NORTH;

        public Direction(char direction){
            _value = direction;
        }

        public char Value => _value;

        public void TurnRight(){
            var directions = GetRightOrientation();
            _direction = (_direction + 1) % 4;
            _value = directions[_direction];
        }
        public void TurnLeft(){
            var directions = GetLeftOrientation();
            _direction = (_direction + 1) % 4;
            _value = directions[_direction];
        }
        private char[] GetRightOrientation(){
            return new char[]
                {   CardinalPoint.NORTH,
                    CardinalPoint.EAST,
                    CardinalPoint.SOUTH,
                    CardinalPoint.WEST
                };
        }
        private char[] GetLeftOrientation(){
            return new char[]
                {   CardinalPoint.NORTH,
                    CardinalPoint.WEST,
                    CardinalPoint.SOUTH,
                    CardinalPoint.EAST
                };
        } 
    }
}
