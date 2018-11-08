using System.IO;
using System.Threading;
using System.Windows;

namespace kata.GameOfLife.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btn1.Click += btn1_Click;

            MainThread = SynchronizationContext.Current;
            if (MainThread == null)
                MainThread = new SynchronizationContext();
        }

        public SynchronizationContext MainThread { get; }
        public Thread BackgroundThread { get; private set; }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var evolutions = int.Parse(txt1.Text);
            var resolution = 5;
            var canvasWidthSize = 200;
            var canvasHeightSize = 400;
            canvas1.Width = canvasWidthSize;
            canvas1.Height = canvasHeightSize;
            
            grid1.Width = canvasHeightSize + 20;
            mainGrid.Width = canvasHeightSize *  1.1;
            grid1.Height = canvasWidthSize * 1.1;
            
            var cols = canvasWidthSize / resolution;
            var rows = canvasHeightSize / resolution;
            
            var device = new CanvasRenderingDevice(MainThread, canvas1, resolution);
            var grid = new GameOfLife.Grid(device)
                            .WithRandomData(new GridSize(rows, cols));
            var game = new Game(grid, evolutions);

            BackgroundThread = new Thread(
                new ThreadStart(() => {
                    game.Perform();
                })
            );

            BackgroundThread.Start();
        }
    }
}
