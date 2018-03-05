using NUnit.Framework;
using kata.rover;

namespace kata.rover.tests
{
    public class RoverShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]  
        [TestCase("R","0:0:E")]
        [TestCase("RR","0:0:S")]
        [TestCase("RRR","0:0:W")]
        [TestCase("RRRR","0:0:N")]
        public void rotate_right(string commands, string expected)
        {
            Rover rover = new Rover();
            string result = rover.Execute(commands:commands);

            Assert.AreEqual(expected,result);
        }

        [Test]  
        [TestCase("L","0:0:W")]
        [TestCase("LL","0:0:S")]
        [TestCase("LLL","0:0:E")]
        [TestCase("LLLL","0:0:N")]
        public void rotate_left(string commands, string expected)
        {
            Rover rover = new Rover();
            string result = rover.Execute(commands:commands);
            Assert.AreEqual(expected,result);
        }

        [Test]
        [TestCase("M","0:1:N")]
        [TestCase("MM","0:2:N")]
        [TestCase("MMM","0:3:N")]
        [TestCase("MMMM","0:4:N")]
        public void move_up(string commands, string expected)
        {
            Rover rover = new Rover();
            string result = rover.Execute(commands:commands);
            Assert.AreEqual(expected,result);
        }

        
        [Test]
        [TestCase("RM","1:0:E")]
        [TestCase("RMM","2:0:E")]
        [TestCase("RMMM","3:0:E")]
        [TestCase("RMMMM","4:0:E")]
        public void move_east(string commands, string expected)
        {
            Rover rover = new Rover();
            string result = rover.Execute(commands:commands);
            Assert.AreEqual(expected,result);
        }

        [Test]
        [TestCase("MMMMM",4,4,"0:1:N")]
        public void wrap_around_grid_moving_north(string commands, int maxX, int maxY, string position)
        {
            var grid = new kata.rover.Grid(maxX, maxY);  
            Rover rover = new Rover(grid);
            string result = rover.Execute(commands:commands);
            Assert.AreEqual(position,result);  
        }

        [Test]
        [TestCase("RRM",4,4,"0:3:S")]
        public void wrap_around_grid_moving_south(string commands, int maxX, int maxY, string position)
        {
            var grid = new kata.rover.Grid(maxX, maxY);  
            Rover rover = new Rover(grid);
            string result = rover.Execute(commands:commands);
            Assert.AreEqual(position,result);  
        }

        [Test]
        [TestCase("RMMLM","2:1:N")]
        public void move_as_expected(string commands, string position)
        {
            Rover rover = new Rover();
            string result = rover.Execute(commands:commands);
            Assert.AreEqual(position,result);
        }
    }
}