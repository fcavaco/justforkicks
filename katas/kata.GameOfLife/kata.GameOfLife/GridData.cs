using System;

namespace kata.GameOfLife
{
    internal class GridData
    {
        private readonly CellStateEnum[,] _data;
        private IInfrastructureDevice<CellStateEnum[,]> _iodevice;

        internal GridData(IInfrastructureDevice<CellStateEnum[,]> iodevice)
        {
            _iodevice = iodevice;
            _data = GetDeviceData();
        }
        
        internal GridData(IInfrastructureDevice<CellStateEnum[,]> iodevice, CellStateEnum[,] data):this(iodevice)
        {
            _data = data;
        }

        private const int ROW_DIMENSION = 0;
        private const int COL_DIMENSION = 1;
        public int NumberRows => _data.GetLength(ROW_DIMENSION);
        public int NumberColumns => _data.GetLength(COL_DIMENSION);

        public void Output() => _iodevice.Output(_data);

        private const int NEIGHBORHOOD_SIZE = 9;
        internal CellStateEnum[] NeighborhoodFor(CellPosition position)
        {
            var context = new CellStateEnum[NEIGHBORHOOD_SIZE];

            for (int pos = 0; pos < NEIGHBORHOOD_SIZE; pos++)
            {
                context[pos] = GetRelativePositionState(position, pos);
            }

            return context;
        }
        internal GridData Randomize(GridSize gridSize)
        {
            var newData = new CellStateEnum[gridSize.NumberRows, gridSize.NumberCols];

            var numberCells = gridSize.NumberRows * gridSize.NumberCols;

            Random randomizer = new Random();
            for (int cellPos = 0; cellPos < numberCells; cellPos++)
            {
                var rowPos = IncrementEvery(cellPos, gridSize.NumberCols);
                var colPos = cellPos % gridSize.NumberCols;
                newData[rowPos, colPos] = GetRandomState(randomizer);
            }

            return new GridData(_iodevice, newData);

        }

        #region private methods
        private CellStateEnum GetRandomState(Random randomizer)
        {
            
            double factor = (randomizer.Next(0, 2));
            return (CellStateEnum)Math.Floor(factor);
        }
        
        private CellStateEnum[,] GetDeviceData()
        {
            var input = _iodevice.Input();

            if (input != null && input.Length > 0)
                return input;

            return new CellStateEnum[1, 1];
        }
        private int IncrementEvery(int current, int skip)
        {
            int rem;
            int quotient = Math.DivRem(current, skip, out rem);
            return quotient;
        }

        private int ColShift(int current)
        {
            const int LEFT = -1;
            const int CENTER = 0;
            const int RIGHT = 1;

            if ((current) == 1) return CENTER;
            if ((current) == 2) return RIGHT;

            return LEFT;
        }
        
        private CellStateEnum GetRelativePositionState(CellPosition position, int relativePosition)
        {
            const int TOP = -1;
            var colShift = ColShift(relativePosition % 3);
            var shiftAdjust = IncrementEvery(relativePosition, 3);
            var rowShift = TOP + shiftAdjust;

            var col = (position.Col + colShift + NumberColumns) % NumberColumns;
            var row = (position.Row + rowShift + NumberRows) % NumberRows;

            return _data[row, col];
        }
        #endregion private methods
    }
}