namespace kata.GameOfLife
{
    public class GridSize
    {
        private readonly int _nrows;
        private readonly int _ncols;
        public GridSize(int nrows, int ncols)
        {
            _nrows = nrows;
            _ncols = ncols;
        }

        public int NumberRows => _nrows;
        public int NumberCols => _ncols;
    }
}