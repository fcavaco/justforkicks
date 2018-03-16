using System;
using System.Collections.Generic;

namespace kata.GameOfLife
{
    public class Grid
    {
        private GridData _data;
        private IInfrastructureDevice<CellStateEnum[,]> _iodevice;

        public Grid(IInfrastructureDevice<CellStateEnum[,]> iodevice)
        {
            _iodevice = iodevice;
            _data = new GridData(_iodevice);
        }

       
        // this is just for internal use of the Grid
        private Grid(CellStateEnum[,] data, IInfrastructureDevice<CellStateEnum[,]> iodevice):this(iodevice)
        {
            _data = new GridData(iodevice, data);
        }

        public Grid Evolve()
        {
            var nrows = _data.NumberRows;
            var ncols = _data.NumberColumns;

            if (nrows < 3 || ncols < 3)
                throw new IllegalGridException("3x3 is min grid size allowed.");
            
            var cells = evolveGridCells(nrows,ncols);

            return new Grid(cells, _iodevice);
        }

        public void Output()
        {
            _data.Output();
        }

        #region private methods
        private CellStateEnum[,] evolveGridCells(int nrows, int ncols)
        {
            var newData = new CellStateEnum[nrows, ncols];

            for (int row = 0; row < nrows; row++)
            {
                evolveRowCells(row, newData);
            }

            return newData;
        }

        private void evolveRowCells(int row, CellStateEnum[,] state)
        {
            var cols  = _data.NumberColumns;
            for (int col = 0; col < cols; col++)
            {
                var cellContext = _data.NeighborhoodFor(new CellPosition(row, col));
                var cell = new Cell(cellContext);
                state[row,col] = cell.Evolve();
            }
        }

        public Grid WithRandomData(GridSize gridSize)
        {
            _data = _data.Randomize(gridSize);
            
            return this;
        }

        #endregion private methods
    }
}