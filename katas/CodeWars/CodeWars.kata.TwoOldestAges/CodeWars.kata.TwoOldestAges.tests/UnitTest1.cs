namespace Solution
{
    using NUnit.Framework;

    [Parallelizable(ParallelScope.Children)]
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test5()
        {
            int[] result = LargestTwo.TwoOldestAges(new[] { 1, 2 });
            Assert.AreEqual(new int[] { 1, 2 }, result);
        }
        [Test]
        public void Test6()
        {
            int[] result = LargestTwo.TwoOldestAges(new[] { 2, 1 });
            Assert.AreEqual(new int[] { 1, 2 }, result);
        }
        [Test]
        public void Test7()
        {
            int[] result = LargestTwo.TwoOldestAges(new[] { 312, 93, 152, 390, 315, 370, 180, 259, 165 });
            Assert.AreEqual(new int[] { 370, 390 }, result);
            result = LargestTwo.TwoOldestAges(new[] { 1, 2, 10, 8 });
            Assert.AreEqual(new int[] { 8, 10 }, result);
            result = LargestTwo.TwoOldestAges(new[] { 2, 1 });
            Assert.AreEqual(new int[] { 1, 2 }, result);
        }
        [Test]
        public void Test1()
        {
            int[] result = LargestTwo.TwoOldestAges(new[] { 1, 2, 10, 8 });
            Assert.AreEqual(new int[] { 8, 10 }, result);
        }

        [Test]
        public void Test2()
        {
            int[] result = LargestTwo.TwoOldestAges(new[] { 1, 5, 87, 45, 8, 8 });
            Assert.AreEqual(new int[] { 45, 87 }, result);
        }

        [Test]
        public void Test3()
        {
            int[] result = LargestTwo.TwoOldestAges(new[] { 6, 5, 83, 5, 3, 18 });
            Assert.AreEqual(new int[] { 18, 83 }, result);
        }

        [Test]
        public void Test4()
        {
            int[] result = LargestTwo.TwoOldestAges(new[] { 6, 5, 83, 83 });
            Assert.AreEqual(new int[] { 83, 83 }, result);
        }
    }
}