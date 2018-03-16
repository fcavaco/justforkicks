using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Board3X3 : IBoard
    {
        private Dictionary<Tuple<RowPosition, ColumnPosition>, Marker> placements;
        
        public Board3X3()
        {
            placements = new Dictionary<Tuple<RowPosition, ColumnPosition>, Marker>();
        }
        
        public bool HasRowWin(Marker marker)
        {
            var markerPlacements = placements.Where(x => x.Value == marker);
            var topRowCount = markerPlacements.Count(x => x.Key.Item1 == RowPosition.Top);
            var centerRowCount = markerPlacements.Count(x => x.Key.Item1 == RowPosition.Center);
            var bottomRowCount = markerPlacements.Count(x => x.Key.Item1 == RowPosition.Bottom);

            return topRowCount == 3 || centerRowCount == 3 || bottomRowCount == 3;
        }
        public bool HasColumnWin(Marker marker)
        {
            var markerPlacements = placements.Where(x => x.Value == marker);
            var leftColumnCount = markerPlacements.Count(x => x.Key.Item2 == ColumnPosition.Left);
            var centerColumnCount = markerPlacements.Count(x => x.Key.Item2 == ColumnPosition.Center);
            var rightColumnCount = markerPlacements.Count(x => x.Key.Item2 == ColumnPosition.Right);

            return leftColumnCount == 3 || centerColumnCount == 3 || rightColumnCount == 3;
        }

        public bool IsFilled()
        {
            return placements.Count() == 9;
        }

        public bool HasDiagonalWin(Marker marker)
        {
            var markerPlacements = placements.Where(x => x.Value == marker);
            var firstDiagonal = markerPlacements.Count(x => 
                                                (x.Key.Item1 == RowPosition.Top && x.Key.Item2 == ColumnPosition.Left)
                                                || (x.Key.Item1 == RowPosition.Center && x.Key.Item2 == ColumnPosition.Center)
                                                || (x.Key.Item1 == RowPosition.Bottom && x.Key.Item2 == ColumnPosition.Right));
            var secondDiagonal = markerPlacements.Count(x =>
                                                (x.Key.Item1 == RowPosition.Bottom && x.Key.Item2 == ColumnPosition.Left)
                                                || (x.Key.Item1 == RowPosition.Center && x.Key.Item2 == ColumnPosition.Center)
                                                || (x.Key.Item1 == RowPosition.Top && x.Key.Item2 == ColumnPosition.Right));

            return firstDiagonal == 3 || secondDiagonal == 3 ;
        }
        public bool IsPositionTaken(RowPosition row, ColumnPosition column)
        {
            return placements.Count(x => x.Key.Item1 == row && x.Key.Item2 == column) > 0;
        }

        public void Place(Marker marker, RowPosition row, ColumnPosition column)
        {
            this.placements.Add(new Tuple<RowPosition, ColumnPosition>(row, column),marker);
        }

       
    }
}