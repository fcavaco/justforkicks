using kata.LawnMower.WebApi.Controllers;
using NUnit.Framework;

namespace Tests
{
    public class LawnMowerControllerTests
    {
        private LawnMowerController _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LawnMowerController();
        }
        
    }
}