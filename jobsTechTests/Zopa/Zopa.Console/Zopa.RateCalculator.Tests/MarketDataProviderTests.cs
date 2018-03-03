using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Zopa.RateCalculator.Tests
{
    [TestFixture]
    public class MarketDataProviderTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void system_can_instanciate_lendend_amount()
        {
            var item = new LendedAmount("john", 00.069M, 800.00M);
            Assert.AreEqual(800.00M, item.Amount);
            Assert.AreEqual("john", item.Lender);
            Assert.AreEqual(00.069M, item.Rate);
        }
        [Test]
        [TestCase(1000.00,2)]
        [TestCase(1100.00, 4)]
        [TestCase(1200.00, 4)]
        [TestCase(1300.00, 5)]
        [TestCase(1400.00, 5)]
        [TestCase(1500.00, 5)]
        public void I_can_select_list_best_loans(decimal borrowed, int selectedCount)
        {
            var mockProvider = new Mock<MarketDataProvider>();
            mockProvider.SetupGet(x => x.AvailableLoans).Returns(GetMockLendedAmounts);

            var sut = new Calculator(mockProvider.Object);

            // minimal rate, and value fits
            var loans = sut.SelectLoans(borrowed);

            Assert.AreEqual(selectedCount,loans.Count());
        }

        [Test]
        [TestCase(1000.00, 0.07004)]
        [TestCase(1100.00, 0.070236364)]
        [TestCase(1200.00, 0.07055)]
        [TestCase(1300.00, 0.070892308)]
        [TestCase(1400.00, 0.071185714)]
        [TestCase(1500.00, 0.07144)]
        public void I_can_get_blended_rate_for_best_loans(decimal borrowed, decimal expectedRate)
        {
            var mockProvider = new Mock<MarketDataProvider>();
            mockProvider.SetupGet(x => x.AvailableLoans).Returns(GetMockLendedAmounts);

            var sut = new Calculator(mockProvider.Object);

            // minimal rate, and value fits
            decimal rate = sut.CalculateBlendedRate(borrowed);

            Assert.AreEqual(Math.Round(expectedRate,4), Math.Round(rate,4));
        }

        [Test]
        public void Will_throw_exception_when_borrowed_value_exceeds_available_provided_amounts()
        {
            var mockProvider = new Mock<MarketDataProvider>();
            mockProvider.SetupGet(x => x.AvailableLoans).Returns(GetMockLendedAmounts);

            var sut = new Calculator(mockProvider.Object);

            var ex = Assert.Throws<Exception>(() =>
            {
                sut.SelectLoans(15000M).Count();
            });
            Assert.That(ex.Message, Is.EqualTo("System cannot provide a quote a this moment."));

        }
        private IList<LendedAmount> GetMockLendedAmounts()
        {
            var list = new List<LendedAmount>();
            list.Add(new LendedAmount("Bob", 0.075M, 640.00M));
            list.Add(new LendedAmount("Jane", 0.069M, 480.00M));
            list.Add(new LendedAmount("Fred", 0.071M, 520.00M));
            list.Add(new LendedAmount("Mary", 0.104M, 170.00M));
            list.Add(new LendedAmount("John", 0.081M, 320.00M));
            list.Add(new LendedAmount("Dave", 0.074M, 140.00M));
            list.Add(new LendedAmount("Angela", 0.071M, 60.00M));
            
            return list;
        }

        [Test]
        [TestCase(1000, 3, 12, 1233.00)]
        [TestCase(1500, 3, 12, 1855.5)]
        public void I_can_calculate_compound_interest_amount_for_X_years(decimal borrowed, int years, int yearlyFractions, decimal expectedDueAmount)
        {
            var mockProvider = new Mock<MarketDataProvider>();
            mockProvider.SetupGet(x => x.AvailableLoans).Returns(GetMockLendedAmounts);

            var sut = new Calculator(mockProvider.Object);

            decimal amountDue = sut.CalculateAmountDue(borrowed, years, yearlyFractions);

            Assert.AreEqual(Math.Round(expectedDueAmount,2), Math.Round(amountDue,2));
        }
    }

    
}
