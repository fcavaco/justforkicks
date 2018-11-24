namespace kata.LawnMower.Models
{
    public interface ISize
    {
        int Lenght { get; }
        int Width { get; }
    }
    public class GardenSize:ISize
    {
        public GardenSize(int lenght, int width)
        {
            Lenght = lenght;
            Width = width;
        }

        public int Lenght { get; private set; }
        public int Width { get; private set; }
    }
}