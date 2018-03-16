using kata.GameOfLife;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;

namespace Tests
{
    public class GridShould
    {
        private StringWriter _writer;
        [SetUp]
        public void Setup()
        {
            _writer = new StringWriter();
        }

        [Test]
        [TestCase(3, 3, "[[0,0,0],[0,1,0],[0,0,0]]")]
        public void Receive_initial_state_and_resolve_grid_dimensions(int expectedRows, int expectedCols, string input)
        {
            var reader = new StringReader(input);
            var device = new JsonStringDevice(_writer, reader);
            Grid grid = new Grid(device);
            grid.Output();

            var output = _writer.ToString();
            var state = JsonConvert.DeserializeObject<int[,]>(output);

            Assert.AreEqual(expectedRows, state.GetLength(0));
            Assert.AreEqual(expectedCols, state.GetLength(1));
        }
        [Test]
        [TestCase("[[0,0,0],[0,1,0],[0,0,0]]", "[[0,0,0],[0,0,0],[0,0,0]]")]
        [TestCase("[[0,0,0],[0,1,0],[0,0,0],[0,0,0]]", "[[0,0,0],[0,0,0],[0,0,0],[0,0,0]]")]
        public void Evolve_based_on_initial_state(string input,string expected)
        {
            var reader = new StringReader(input);
            var device = new JsonStringDevice(_writer, reader);
            Grid grid = new Grid(device);

            Grid newGrid = grid.Evolve();
            newGrid.Output();

            var output = _writer.ToString();
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase("[[0,0,0,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,0,0,0]]", "[[0,0,0,0,0],[0,0,0,0,0],[0,1,1,1,0],[0,0,0,0,0],[0,0,0,0,0]]")]
        [TestCase("[[0,0,0,0,0],[0,0,0,0,0],[0,1,1,1,0],[0,0,0,0,0],[0,0,0,0,0]]", "[[0,0,0,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,1,0,0],[0,0,0,0,0]]")]
        public void Be_able_to_evolve_flippers(string input, string expected)
        {
            var reader = new StringReader(input);
            var device = new JsonStringDevice(_writer, reader);
            Grid grid = new Grid(device);

            Grid newGrid = grid.Evolve();
            newGrid.Output();

            var output = _writer.ToString();
            Assert.AreEqual(expected, output);
        }
        [Test]
        [TestCase("[[0,0,0],[0,1,0]]")]
        public void throw_given_smaller_than_3_by_3_grid(string input)
        {
            var reader = new StringReader(input);
            var device = new JsonStringDevice(_writer, reader);
            Grid grid = new Grid(device);

            var ex = Assert.Throws<IllegalGridException>(() => grid.Evolve());
        }
   
        [Test]
        [TestCase("[[0,0,0],[0,1,0],[0,0,0],[0,0,0]]", "[[0,0,0],[0,1,0],[0,0,0],[0,0,0]]")]
        public void Accept_and_use_IO_Device(string input, string expected)
        {
            var reader = new StringReader(input);
            var device = new JsonStringDevice(_writer, reader);
            Grid grid = new Grid(device);

            grid.Output();

            var output = _writer.ToString();

            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Game_randomly_generate_initial_state_if_state_not_present()
        {
            var reader = new StringReader("");
            var device = new JsonStringDevice(_writer, reader);
            Grid grid = new Grid(device).WithRandomData(new GridSize(10,10));
            
            grid.Output();
            var output = _writer.ToString();
            var state = JsonConvert.DeserializeObject<CellStateEnum[,]>(output);

            Assert.IsTrue(state.GetLength(0) == 10);
            Assert.IsTrue(state.GetLength(1) == 10);

        }

    }
}