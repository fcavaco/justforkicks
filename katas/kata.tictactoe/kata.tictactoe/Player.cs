using System;

namespace Tests
{
    public class Player
    {   
        private readonly Marker playingMarker;
        private readonly TicTacToeGame game;
        
        public Player(TicTacToeGame game,Marker playingMarker)
        {
            this.playingMarker = playingMarker;
            this.game = game;
        }
       
        public void Play(RowPosition row, ColumnPosition column)
        {
            game.AcceptMove(this,row, column);
        }

        internal bool IsForMarker(char markerCharacter)
        {
            return this.playingMarker.Is(markerCharacter);
        }
        public bool Is(Player player)
        {
            return player != null && this.Equals(player);
        }
        public bool Wins()
        {
            return this.Is(game.Winner());
        }
        public bool Loses()
        {
            return this.Is(game.Loser());
        }
        public bool Draws()
        {
            return !this.Is(game.Loser()) && !this.Is(game.Winner());
        }
        internal IllegalPlayException AlternatingMoveException()
        {
            return new IllegalPlayException($"Player for {playingMarker.ToString()} should play next.");
        }
        internal IllegalPlayException FirstMoveException()
        {
            return new IllegalPlayException($"Player for {playingMarker.ToString()} should play first.");
        }

        internal IllegalPlayException CannotPlayAlreadyTakenPositionException()
        {
            return new IllegalPlayException($"Player cannot play on already taken position.");
        }

      
        internal void Mark(IBoard board, RowPosition row, ColumnPosition column)
        {            
            board.Place(this.playingMarker,row,column);           
        }

        internal bool Won(IBoard board)
        {
            return board.HasRowWin(this.playingMarker)
                || board.HasColumnWin(this.playingMarker)
                || board.HasDiagonalWin(this.playingMarker);
        }
        internal bool Lost(IBoard board, Player other)
        {
            var otherWon = other.Won(board);
            var won = this.Won(board);

            if (otherWon && !won)
                return true;

            return false;
        }

        internal bool Draw(IBoard board, Player other)
        {
            var otherWon = other.Won(board);
            var won = this.Won(board);

            return game.IsFinished() && !otherWon && !won;
        }
    }
}