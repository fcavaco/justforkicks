namespace kata.rover
{
    public class Coordinate{
        private int _x = 0;
        private int _y = 0;

        public int X => _x;
        public int Y => _y;

        public Coordinate(int x, int y){
           _x = x;
           _y = y;
        }

        public override string ToString(){
            return $"{this.X.ToString()}:{this.Y.ToString()}";
        }
    
    }
}
