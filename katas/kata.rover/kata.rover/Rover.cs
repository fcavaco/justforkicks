using System;

namespace kata.rover
{
    public class Direction{
        
        int _direction = 0;
        char[] _states = new char[]{'N','E','S','W'};

        char _value = 'N';

        public Direction(){
            _value = 'N';
        }
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
            return new char[]{'N','E','S','W'};
        }
        private char[] GetLeftOrientation(){
            return new char[]{'N','W','S','E'};
        } 
    }
    public class Rover    
    {
        private int[] _position = new int[]{0,0};

        private Direction _direction;
        public Rover(){
            _direction = new Direction();
        }

        public string Execute(string commands)
        {
            
            foreach(char command in commands.ToCharArray())
            {
               if(command == 'R')
               {
                   _direction.TurnRight();                  
               }   

               if(command == 'L')
               {
                   _direction.TurnLeft();                  
               }  
            }
            return "0:0:" + _direction.Value;
        }

        
    }
}
