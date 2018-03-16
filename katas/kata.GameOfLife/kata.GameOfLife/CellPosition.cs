namespace kata.GameOfLife
{
    internal class CellPosition
    {
        internal CellPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }
        internal int Row { get; private set; }
        internal int Col { get; private set; }
    }
}