namespace Tests
{
    public interface IBoard
    {
        bool IsPositionTaken(RowPosition row, ColumnPosition column);
        void Place(Marker marker, RowPosition row, ColumnPosition column);
        bool HasRowWin(Marker marker);
        bool HasColumnWin(Marker marker);
        bool HasDiagonalWin(Marker marker);
        bool IsFilled();
    }
}