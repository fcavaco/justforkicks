
using NUnit.Framework;
using PrizeDraw;
using System;
using System.Linq;
using System.Reflection;

namespace PrizeDraw.Tests
{

    [TestFixture]
    public class PrizeDrawTests
    {
        [Test]
        [TestCase("4 10 5 5 1", 4)]
        [TestCase("1 1", 1)]
        [TestCase("0", 0)]
        public void I_can_retrieve_daily_orders_count(string input, int expected)
        {
            var sut = new DailyPrizeDraw(input);
            Assert.AreEqual(expected, sut.OrderCount);
        }
        [Test]
        [TestCase("4 10 5 5 1", 4)]
        [TestCase("1 1", 1)]
        [TestCase("0", 0)]
        public void I_can_retrieve_daily_orders(string input, int expected)
        {
            var sut = new DailyPrizeDraw(input);
            var items = sut.Items;
            Assert.AreEqual(expected, items.Count());
        }

        [Test]
        [TestCase("4 10 5 5 1", 2, 10)]
        public void I_can_identify_daily_orders_not_matched_for_prizeamount(string input, int expectedCount, int expectedAmount)
        {
            // result is 9: so 10:max and 1:min are out. eligible for further drwas are 5 and 5.
            var sut = new DailyPrizeDraw(input);
            var remaining = sut.Remaining;
            Assert.AreEqual(expectedCount, remaining.Count());
            Assert.AreEqual(expectedAmount, remaining.Sum());
        }

        [Test]
        [TestCase("4 10 5 5 1", 9)]
        public void I_can_calculate_prize_money_for_one_day(string input, int expected)
        {
            var sut = new DailyPrizeDraw(input);
            Assert.AreEqual(expected, sut.CalculatePrizeAmount());
        }

        [Test]
        [TestCase("1 1", 4)]
        public void I_can_carry_over_remaining_to_next_day(string input, int expected)
        {
            var previousDay = new DailyPrizeDraw("4 10 5 5 1"); // 5 and 5 remain as eligible to following day
            var sut = new DailyPrizeDraw(input, previousDay);

            // so : 1 & 5 & 5 => 5 - 1 = 4
            Assert.AreEqual(expected, sut.CalculatePrizeAmount());
        }

        [Test]
        [TestCase("PrizeDraw.Tests.TestFiles.FiServ_PrizeDraw_TestInput.txt", 19)]
        public void I_can_calculate_campaign_prize_amount(string resource, int expected)
        {
            // retrieve text from embeded resource file.
            var assembly = Assembly.GetExecutingAssembly();
            var input = new EmbededResource(assembly)
                                .Get(resource)
                                .ContentAsArray;

            var sut = new CampaignPrizeDraw(input);

            var prizeAmount = sut.CalculatePrizeAmount();

            Assert.AreEqual(expected, prizeAmount);
        }
        
        [Test]
        [TestCase("100001 1 2 3 4 5 6 7 8", "number of registered daily orders is invalid: 0 <= r <= 100000.")]
        [TestCase("-1 234", "number of registered daily orders is invalid: 0 <= r <= 100000.")]
        public void system_throws_exception_for_invalid_number_of_orders(string input, string expected)
        {
            var sut = new DailyPrizeDraw(input);

            var ex = Assert.Throws<Exception>(() => sut.CalculatePrizeAmount());
            Assert.AreEqual(expected, ex.Message);

        }

        //TODO: order size should not exceed X

        [Test]
        [TestCase("PrizeDraw.Tests.TestFiles.FiServ_PrizeDraw_TestInputException1.txt", "registered number of days is not allowed. positive integer d such that 1 <= d <= 5000.")]
        [TestCase("PrizeDraw.Tests.TestFiles.FiServ_PrizeDraw_TestInputException2.txt", "registered number of days is not allowed. positive integer d such that 1 <= d <= 5000.")]
        [TestCase("PrizeDraw.Tests.TestFiles.FiServ_PrizeDraw_TestInputException3.txt", "registered number of days is not allowed. positive integer d such that 1 <= d <= 5000.")]
        [TestCase("PrizeDraw.Tests.TestFiles.FiServ_PrizeDraw_TestInputException4.txt", "number of orders exceeds campaign limit of 100000.")]
        public void system_throws_exception_for_invalid_number_of_days(string resource, string expected)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var input = new EmbededResource(assembly)
                                .Get(resource)
                                .ContentAsArray;


            var sut = new CampaignPrizeDraw(input);

            var ex = Assert.Throws<Exception>(() => sut.CalculatePrizeAmount());
            Assert.AreEqual(expected, ex.Message);
            
        }
       
       

        #region helper methods

        #endregion helper methods
    }
}
