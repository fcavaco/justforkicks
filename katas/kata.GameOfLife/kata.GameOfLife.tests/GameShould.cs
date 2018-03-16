using kata.GameOfLife;
using NUnit.Framework;
using System.IO;

namespace Tests
{
    public class GameShould
    {
        private StringWriter _writer;
        [SetUp]
        public void Setup()
        {
            _writer = new StringWriter();
        }
        
        [Test]
        [TestCase(1, "[[0,0,0,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,0,0,0]]", "[[0,0,0,0,0],[0,0,0,0,0],[0,1,1,1,0],[0,0,0,0,0],[0,0,0,0,0]]")]
        [TestCase(2, "[[0,0,0,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,0,0,0]]", "[[0,0,0,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,0,0,0]]")]
        [TestCase(3, "[[0,0,0,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,0,0,0]]", "[[0,0,0,0,0],[0,0,0,0,0],[0,1,1,1,0],[0,0,0,0,0],[0,0,0,0,0]]")]
        public void Perform_n_life_evolutions(int evolutions, string input, string expected)
        {
            // using the flipper pattern to assert n iterations been achieved.
            // so for example...  | --- | --- (implies 3 evolutions after initial state for a 5x5 grid)
            var reader = new StringReader(input);
            var device = new JsonStringDeviceForTestingGame(_writer, reader);
            var grid = new Grid(device);
            var game = new Game(grid, evolutions);

            game.Perform();
            
            var temp = _writer.ToString();
            var outputs = temp.Split('#');
            var output  = outputs[outputs.Length - 1];
            Assert.AreEqual(expected, output);
        }
    }
}