namespace kata.LawnMower.Models
{
    public interface IPosition
    {
        int YAxis { get; }
        int XAxis { get; }
        char Orientation { get; }

        IPosition Move(ISize withinBoundary);
        IPosition Turn(char newOrientation);
    }
    public static class Oriented
    {
        public const char NORTH = 'N';
        public const char EAST = 'E';
        public const char SOUTH = 'S';
        public const char WEST = 'W';
    }
    public class DevicePosition:IPosition
    {
        public DevicePosition(int y, int x, char orientation)
        {
            YAxis = y;
            XAxis = x;
            Orientation = orientation;
        }

        public int YAxis { get; private set; }
        public int XAxis { get; private set; }
        public char Orientation { get; private set; }

        public IPosition StepNorth()
        {
            return new DevicePosition(this.YAxis + 1, this.XAxis, this.Orientation);
        }

        public IPosition StepSouth()
        {
            return new DevicePosition(this.YAxis - 1, this.XAxis, this.Orientation);
        }

        public IPosition StepEast()
        {
            return new DevicePosition(this.YAxis, this.XAxis + 1, this.Orientation);
        }

        public IPosition StepWest()
        {
            return new DevicePosition(this.YAxis, this.XAxis - 1, this.Orientation);
        }

        public IPosition Move(ISize withinBoundary)
        {
            IPosition newPosition = this;
            switch (this.Orientation)
            {
                case Oriented.NORTH:
                    newPosition = this.YAxis < withinBoundary.Lenght - 1 ? StepNorth(): this;
                    break;
                case Oriented.EAST:
                    newPosition = this.XAxis < withinBoundary.Width - 1 ? StepEast() : this;
                    break;
                case Oriented.SOUTH:
                    newPosition = this.YAxis > 0 ? StepSouth() : this;
                    break;
                case Oriented.WEST:
                    newPosition = this.XAxis > 0 ? StepWest() : this;
                    break;
            };

            return newPosition;
        }

        public IPosition Turn(char orientation)
        {
            return new DevicePosition(this.YAxis, this.XAxis, orientation);
        }
    }
}