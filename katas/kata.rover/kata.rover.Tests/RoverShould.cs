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
    }
}