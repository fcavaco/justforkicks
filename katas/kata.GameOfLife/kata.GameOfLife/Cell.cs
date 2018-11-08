using System;

namespace kata.GameOfLife
{
    // note this class does not need to be visible outside internal assembly context.
    public class Cell
    {
        private CellStateEnum[] _context;
        private const int CELL_POSITION = 4;
        
        private CellStateEnum _state;
        public Cell( CellStateEnum[] context)
        {
            _context = context;
            _state = _context[CELL_POSITION];
        }

        public CellStateEnum Evolve()
        {
            var aliveNeighbourCount = GetAliveNeighboursCount();

            // cell rule : under-population if alive then dies
            if (_state == CellStateEnum.Alive && aliveNeighbourCount < 2)
                _state = CellStateEnum.Dead;

            if (_state == CellStateEnum.Alive && (aliveNeighbourCount == 3 || aliveNeighbourCount == 2))
                _state = CellStateEnum.Alive;

            // cell rule : over-population if alive then dies
            if (_state == CellStateEnum.Alive && aliveNeighbourCount > 3)
                _state = CellStateEnum.Dead;

            if (_state == CellStateEnum.Dead && aliveNeighbourCount == 3)
                _state = CellStateEnum.Alive;



            return _state;

        }
        #region private methods
        private int GetAliveNeighboursCount()
        {
            var result = 0;
            for (int i = 0; i < 9; i++)
            {
                result += (int)_context[i];
            }
            result -= (int)_context[CELL_POSITION];
            return result;
        }
        #endregion private methods
    }
}