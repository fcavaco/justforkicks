using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread BackgroundThread;
        private SynchronizationContext MainThread;

        public MainWindow()
        {
            InitializeComponent();
            btn1.Click += btn1_Click;

            MainThread = SynchronizationContext.Current;
            if (MainThread == null)
                MainThread = new SynchronizationContext();
        }
        

        private void btn1_Click(object sender, RoutedEventArgs e)
        {

            var numberCycles = int.Parse(txt1.Text);
            var resolution = 10;
            var canvasWidthSize = 600;
            var canvasHeightSize = 400;
            canvas1.Width = canvasWidthSize;
            canvas1.Height = canvasHeightSize;
            
            grid1.Width = canvasWidthSize + 20;
            mainGrid.Width = canvasWidthSize + 40;
            grid1.Height = canvasHeightSize + 20;
            
            var cols = canvasWidthSize / resolution;
            var rows = canvasHeightSize / resolution;

            BackgroundThread = new Thread(new ThreadStart(() => {
                // create the initial grid
                int[,] grid = SetInitialGrid(cols, rows);
               
                for (int i = 0; i < numberCycles; i++)
                {
                    // draw grid generation
                    DrawGeneration(grid, cols, rows, resolution);

                    // compute new grid according to Conway's rules
                    grid = ComputeNewGeneration(grid, cols, rows);

                    // allow sync context to main thread
                    Thread.Sleep(100);
                }
                
            }));
            BackgroundThread.Start();
            
        }

        private int[,] ComputeNewGeneration(int[,] grid, int cols, int rows)
        {
            int[,] newgrid = new int[cols, rows];

            for(int i = 0 ; i < cols ; i++)
            {
                for(int j = 0 ; j < rows ; j++)
                {
                    // keep current cell state
                    var state = grid[i, j];

                    // determine number of live neighbours
                    var neighbours = CountLiveNeighbours(grid, i, j, cols, rows); 

                    // apply conway's rules
                    if(state == 0 && neighbours == 3)
                    {
                        newgrid[i, j] = 1;
                    }
                    else if(state==1 && (neighbours < 2 || neighbours > 3))
                    {
                        newgrid[i, j] = 0;
                    }
                    else
                    {
                        newgrid[i, j] = state;
                    }

                }
            }

            return newgrid;
        }
        private int CountLiveNeighbours(int[,] grid, int x, int y, int cols, int rows)
        {
            var sum = 0;

            for (int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    // allow wrapping around 2d grid (for edge cells)
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    // sum up state of neighbour cell
                    sum += grid[col, row];
                }
            }

            // discount state for cell being evaluated
            sum -= grid[x, y];

            return sum;
        }
        private int[,] SetInitialGrid(int cols, int rows)
        {
            int[,] grid = new int[cols, rows];

            // set initial random value to each grid cell
            Random r = new Random();
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    // set random 1 or 0 to the cell
                    double factor = (r.Next(0, 2));
                    grid[i, j] = (int)Math.Floor(factor);
                }
            }

            return grid;
        }

        private void DrawGeneration(int[,] grid, int cols, int rows, int resolution)
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    // allow scaling according to canvas size
                    var x = i * resolution;
                    var y = j * resolution;
                   
                    if (grid[i, j] == 1)
                    {
                        // cell is alive - draw filled black

                        // allow context sync to the main thread.
                        MainThread.Post((object state) =>
                        {
                            DrawFilledSquare(x, y, resolution);
                        }, null);
                    }
                    else
                    {
                        // cell is dead - draw filled white

                        MainThread.Post((object state) =>
                        {
                            DrawSquare(x, y, resolution);
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
            
                Rectangle rect = new Rectangle { Width = resolution, Height = resolution, Stroke = color, Fill=Brushes.White };
                canvas1.Children.Add(rect);
                Canvas.SetTop(rect, y);
                Canvas.SetLeft(rect, x);
        }
        

        private void DrawFilledSquare(int x, int y, int resolution, Brush color = null)
        {
            if (color == null)
                color = Brushes.Black;

                Rectangle rect = new Rectangle { Width = resolution, Height = resolution, Fill = color };
                canvas1.Children.Add(rect);
                Canvas.SetTop(rect, y);
                Canvas.SetLeft(rect, x);
        }
    }

   
}
