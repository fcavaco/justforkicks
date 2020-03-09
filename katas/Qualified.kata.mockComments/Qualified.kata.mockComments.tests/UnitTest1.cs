using NUnit.Framework;

namespace Qualified.kata.mockComments.tests
{
[TestFixture]
public class OOPTests {
  [Test]
  public void TestInstantiation() {
    Challenge.User user = new Challenge.User("User 1");
    Assert.AreEqual( "User 1", user.GetName(),"User name is set correctly:");
    Challenge.Moderator mod = new Challenge.Moderator("Moderator");
    Assert.IsInstanceOf<Challenge.User>( mod ,"Moderator is a User:");
  }
}
}