using System.Threading;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace kata.GameOfLife.Wpf
{
    public class CanvasRenderingDevice : IInfrastructureDevice<CellStateEnum[,]>
    {
        private readonly SynchronizationContext _sc;
        private readonly Canvas _canvas;
        private readonly int _resolution;
        public CanvasRenderingDevice(SynchronizationContext context, Canvas canvas, int resolution)
        {
            _sc = context;
            _canvas = canvas;
            _resolution = resolution;

        }
        public CellStateEnum[,] Input()
        {
            return null;
        }

        public void Output(CellStateEnum[,] state)
        {
            DrawGeneration(state, state.GetLength(0), state.GetLength(1));
        }

        private void DrawGeneration(CellStateEnum[,] grid, int cols, int rows)
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    // allow scaling according to canvas size
                    var x = i * _resolution;
                    var y = j * _resolution;

                    if (grid[i, j] == CellStateEnum.Alive)
                    {
                        // cell is alive - draw filled black

                        // allow context sync to the main thread.
                        _sc.Post((object state) =>
                        {
                            DrawFilledSquare(x, y, _resolution);
                        }, null);
                    }
                    else
                    {
                        // cell is dead - draw filled white

                        _sc.Post((object state) =>
                        {
                            DrawSquare(x, y, _resolution);
                        }, null);
                    }

                    Thread.Sleep(1);
                }
            }
        }
        private void DrawSquare(int x, int y, int resolution, Brush color = null)
        {
            if (color == null)
                color = Brushes.Black;

            Rectangle rect = new Rectangle { Width = resolution, Height = resolution, Stroke = color, Fill = Brushes.White };
            _canvas.Children.Add(rect);
            Canvas.SetTop(rect, y);
            Canvas.SetLeft(rect, x);
        }


        private void DrawFilledSquare(int x, int y, int resolution, Brush color = null)
        {
            if (color == null)
                color = Brushes.Black;

            Rectangle rect = new Rectangle { Width = resolution, Height = resolution, Fill = color };
            _canvas.Children.Add(rect);
            Canvas.SetTop(rect, y);
            Canvas.SetLeft(rect, x);
        }
    }
}
