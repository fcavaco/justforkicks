using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class TicTacToeGame
    {
        private readonly IBoard board;
        private int numberMoves = 0;
        private Player currentPlayer;
        private List<Player> players;

        public TicTacToeGame(IBoard board)
        {
            this.board = board;
            players = new List<Player>();
        }

        public Player AddPlayerFor(Marker marker)
        {
            var player = new Player(this, marker);
            players.Add(player);
            return player;
        }
        
        internal void AcceptMove(Player player, RowPosition row, ColumnPosition column)
        {
            ValidateFirstMove(player);

            ValidateAlternatingMoves(player);

            ValidateExistingTakenPositions(player, row, column);

            player.Mark(board, row, column);

            currentPlayer = player;
            numberMoves++;
        }

        

        public bool IsFinished()
        {
            return board.IsFilled() || players.Count(x=>x.Won(board)) > 0;
        }

        internal Player Winner()
        {
            return players.FirstOrDefault(x => x.Won(board));
        }

        internal Player Loser()
        {
            var winner = players.FirstOrDefault(x => x.Won(board));

            if(winner != null)
            {
                var otherPlayer = ShiftPlayer(winner);
                return otherPlayer;
            }
            return null;
        }

        #region private methods
        private void ValidateExistingTakenPositions(Player player, RowPosition row, ColumnPosition column)
        {
            //? probably advised to push this validation to the board...

            if (board.IsPositionTaken(row, column))
            {
                throw player.CannotPlayAlreadyTakenPositionException();
            }
        }

        private void ValidateAlternatingMoves(Player player)
        {
            if (player.Is(currentPlayer))
            {
                var otherPlayer = ShiftPlayer(player);
                throw otherPlayer.AlternatingMoveException();
            }
        }

        private void ValidateFirstMove(Player player)
        {
            if (IsInvalidFirstMove(player))
            {
                var otherPlayer = ShiftPlayer(player);
                throw otherPlayer.FirstMoveException();
            }
        }
        private bool HasMoves()
        {
            return numberMoves > 0;
        }

        private Player ShiftPlayer(Player player)
        {
            var otherPlayer = players.Find(x => !x.Equals(player));
            return otherPlayer;
        }

        private bool IsInvalidFirstMove(Player player)
        {
            return !this.HasMoves() && !player.IsForMarker('X');
        }

        #endregion private methods
    }
}