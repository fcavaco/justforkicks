using NUnit.Framework;

namespace CodeWars.Kata.TakeTenMinuteWalk.tests
{
  using NUnit.Framework;
  using System;

  [TestFixture]
  public class SolutionTest
  {
    [Test]
    public void SampleTest()
    {
        Assert.AreEqual(true, Kata.IsValidWalk(new string[] {"n","s","n","s","n","s","n","s","n","s"}), "should return true");
        Assert.AreEqual(false, Kata.IsValidWalk(new string[] {"w","e","w","e","w","e","w","e","w","e","w","e"}), "should return false");
        Assert.AreEqual(false, Kata.IsValidWalk(new string[] {"w"}), "should return false");
        Assert.AreEqual(false, Kata.IsValidWalk(new string[] {"n","n","n","s","n","s","n","s","n","s"}), "should return false");

    }
    [Test]
    public void SampleTest1()
    {
           // a few more just to prove it works
        Assert.AreEqual(true, Kata.IsValidWalk(new string[] {"w","w","s","e","e","n","s","w","e","n"}), "should return true");
        Assert.AreEqual(false, Kata.IsValidWalk(new string[] {"w","w","s","e","e","n","s","w","e","w"}), "should return false");      

    }
  }
}