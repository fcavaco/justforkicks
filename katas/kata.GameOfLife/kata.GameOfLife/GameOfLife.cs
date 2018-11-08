using System;
using System.Collections.Generic;
using System.Text;
using kata.GameOfLife;

namespace kata.GameOfLife
{
    public class Game
    {
        private Grid _grid;
        private int _nEvolutions;

        public Game(Grid grid, int nEvolutions)
        {
            _grid = grid;
            _nEvolutions = nEvolutions;
        }

        public void Perform()
        {
            for (int i = 0; i < _nEvolutions; i++)
            {
                _grid = _grid.Evolve();
                _grid.Output();
            }      
        }
        
    }
}
