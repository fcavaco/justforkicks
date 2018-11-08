using NUnit.Framework;

namespace Tests
{
    public class GameTests
    {

        private Board3X3 board;
        private TicTacToeGame game;
        private Player player1;
        private Player player2;

        [SetUp]
        public void Setup()
        {
            
            board = new Board3X3();
            game = new TicTacToeGame(board);

            player1 = game.AddPlayerFor(new Marker('X'));
            player2 = game.AddPlayerFor(new Marker('O'));
        }

        [Test]
        public void Player_throws_for_x_not_playing_first()
        {
            var ex = Assert.Throws<IllegalPlayException>(() => player2.Play(RowPosition.Top, ColumnPosition.Left));
            Assert.AreEqual("Player for X should play first.", ex.Message);
        }

        [Test]
        public void Player_throws_if_x_play_not_alternated_with_o_play()
        {
            player1.Play(RowPosition.Top, ColumnPosition.Left);

            var ex = Assert.Throws<IllegalPlayException>(() => player1.Play(RowPosition.Top, ColumnPosition.Center));
            Assert.AreEqual("Player for O should play next.", ex.Message);
        }

        [Test]
        public void Player_throws_if_o_play_not_alternated_with_x_play()
        {
            player1.Play(RowPosition.Top, ColumnPosition.Left);
            player2.Play(RowPosition.Top, ColumnPosition.Center);

            var ex = Assert.Throws<IllegalPlayException>(() => player2.Play(RowPosition.Top, ColumnPosition.Right));
            Assert.AreEqual("Player for X should play next.", ex.Message);
        }

        [Test]
        public void Player_cannot_play_on_already_taken_position()
        {
            player1.Play(RowPosition.Top, ColumnPosition.Left);
            var ex = Assert.Throws<IllegalPlayException>(() => player2.Play(RowPosition.Top, ColumnPosition.Left));
            Assert.AreEqual("Player cannot play on already taken position.", ex.Message);
        }
        [Test]
        public void Player_wins_when_has_3_consecutive_horizontal_markers()
        {
            player1.Play(RowPosition.Top, ColumnPosition.Left);
            player2.Play(RowPosition.Bottom, ColumnPosition.Left);
            player1.Play(RowPosition.Top, ColumnPosition.Center);
            player2.Play(RowPosition.Bottom, ColumnPosition.Center);
            player1.Play(RowPosition.Top, ColumnPosition.Right);

            Assert.IsTrue(game.IsFinished());
            Assert.IsTrue(player1.Wins());
        }
        [Test]
        public void Player_wins_when_has_3_consecutive_vertical_markers()
        {
            player1.Play(RowPosition.Top, ColumnPosition.Left);
            player2.Play(RowPosition.Bottom, ColumnPosition.Right);
            player1.Play(RowPosition.Center, ColumnPosition.Left);
            player2.Play(RowPosition.Bottom, ColumnPosition.Center);
            player1.Play(RowPosition.Bottom, ColumnPosition.Left);

            Assert.IsTrue(game.IsFinished());
            Assert.IsTrue(player1.Wins());
        }
        [Test]
        public void Player_wins_when_has_3_consecutive_diagonal_markers_first()
        {
            player1.Play(RowPosition.Top, ColumnPosition.Left);
            player2.Play(RowPosition.Top, ColumnPosition.Right);
            player1.Play(RowPosition.Center, ColumnPosition.Center);
            player2.Play(RowPosition.Bottom, ColumnPosition.Center);
            player1.Play(RowPosition.Bottom, ColumnPosition.Right);

            Assert.IsTrue(game.IsFinished());
            Assert.IsTrue(player1.Wins());
        }
        [Test]
        public void Player_wins_when_has_3_consecutive_diagonal_markers_second()
        {
            player1.Play(RowPosition.Bottom, ColumnPosition.Left);
            player2.Play(RowPosition.Center, ColumnPosition.Right);
            player1.Play(RowPosition.Center, ColumnPosition.Center);
            player2.Play(RowPosition.Bottom, ColumnPosition.Center);
            player1.Play(RowPosition.Top, ColumnPosition.Right);

            Assert.IsTrue(game.IsFinished());
            Assert.IsTrue(player1.Wins());
        }
        [Test]
        public void Player_wins_imply_other_player_loses()
        {
            player1.Play(RowPosition.Bottom, ColumnPosition.Left);
            player2.Play(RowPosition.Center, ColumnPosition.Right);
            player1.Play(RowPosition.Center, ColumnPosition.Center);
            player2.Play(RowPosition.Bottom, ColumnPosition.Center);
            player1.Play(RowPosition.Top, ColumnPosition.Right);

            Assert.IsTrue(game.IsFinished());
            Assert.IsTrue(player1.Wins());
            Assert.IsTrue(player2.Loses());
        }

        [Test]
        public void Player_all_positions_taken_and_both_draw()
        {
            player1.Play(RowPosition.Top, ColumnPosition.Left);
            player2.Play(RowPosition.Center, ColumnPosition.Left);
            player1.Play(RowPosition.Top, ColumnPosition.Center);
            player2.Play(RowPosition.Center, ColumnPosition.Center);
            player1.Play(RowPosition.Center, ColumnPosition.Right);
            player2.Play(RowPosition.Top, ColumnPosition.Right);
            player1.Play(RowPosition.Bottom, ColumnPosition.Left);
            player2.Play(RowPosition.Bottom, ColumnPosition.Center);
            player1.Play(RowPosition.Bottom, ColumnPosition.Right);

            Assert.IsTrue(game.IsFinished());
            Assert.IsTrue(player1.Draws());
            Assert.IsTrue(player2.Draws());
        }
    }
}