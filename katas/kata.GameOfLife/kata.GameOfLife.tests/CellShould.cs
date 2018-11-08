using kata.GameOfLife;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class CellShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(CellStateEnum.Dead, "[0,0,0,0,1,0,0,0,0]")]
        [TestCase(CellStateEnum.Dead, "[0,0,0,1,1,0,0,0,0]")]
        [TestCase(CellStateEnum.Alive, "[0,0,0,1,1,1,0,0,0]")]
        [TestCase(CellStateEnum.Dead, "[0,0,0,1,0,1,0,0,0]")]
        public void When_alive_dies_due_to_under_population(CellStateEnum expected, string jsonContext)
        {
            // Any live cell with fewer than two live neighbours dies, as if caused by under-population.

            var context = JsonConvert.DeserializeObject<CellStateEnum[]>(jsonContext);
            Cell cell = new Cell(context);

            var actual = cell.Evolve();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(CellStateEnum.Dead, "[0,0,1,1,1,1,1,0,0]")]
        [TestCase(CellStateEnum.Dead, "[1,1,1,1,1,1,1,1,1]")]
        public void When_alive_with_more_than_three_neighbours_dies_due_over_population(CellStateEnum expected, string jsonContext)
        {
            // Any live cell with more than three live neighbours dies, as if by over-population.

            var context = JsonConvert.DeserializeObject<CellStateEnum[]>(jsonContext);
            Cell cell = new Cell(context);
            var actual = cell.Evolve();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        [TestCase(CellStateEnum.Alive, "[0,0,0,1,0,1,0,0,1]")]
        [TestCase(CellStateEnum.Alive, "[0,0,0,0,0,0,1,1,1]")]
        public void When_dead_with_exactly_three_live_neighbours_becomes_alive(CellStateEnum expected, string jsonContext)
        {
            // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            var context = JsonConvert.DeserializeObject<CellStateEnum[]>(jsonContext);
            Cell cell = new Cell(context);
            var actual = cell.Evolve();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(CellStateEnum.Alive, "[0,0,0,1,1,1,0,0,0]")]
        [TestCase(CellStateEnum.Alive, "[0,0,0,1,1,1,1,0,0]")]
        public void When_alive_with_exactly_three_or_two_live_neighbours_remains_alive(CellStateEnum expected, string jsonContext)
        {
            // Any live cell with two or three live neighbours should live on to the next generation.
            var context = JsonConvert.DeserializeObject<CellStateEnum[]>(jsonContext);
            Cell cell = new Cell(context);
            var actual = cell.Evolve();

            Assert.AreEqual(expected, actual);
        }
    }
}