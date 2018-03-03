using System;
using System.Collections.Generic;
using System.Linq;

namespace Zopa.RateCalculator
{
    public class Calculator
    {
        private readonly int _monthsRepay;
        private readonly MarketDataProvider _provider;
        public Calculator(MarketDataProvider provider)
        {
            _provider = provider;
            _monthsRepay = 36;
        }

        public int MonthsRepayment => _monthsRepay;
        public int YearsRepayment => _monthsRepay / 12;

        public IEnumerable<LendedAmount> SelectLoans(decimal borrowed)
        {
            var allocated = 0.00M;
            var loans = _provider.AvailableLoans
                .OrderBy(x => x.Rate);

            var totalAvailable = loans.Sum(x => x.Amount);
            if(totalAvailable < borrowed)
                throw new Exception("System cannot provide a quote a this moment.");

            foreach (var lendedAmount in loans)
            {
                if (allocated >= borrowed) break;

                allocated += lendedAmount.Amount;

                yield return lendedAmount;

                if (lendedAmount.Amount - borrowed >= 0)
                {
                    break;
                }
            }
        }

        public decimal CalculateBlendedRate(decimal borrowed)
        {
            var items = new List<LendedAmount>();
            var loans = SelectLoans(borrowed);
            var allocated = 0.000M;

            foreach (var lendedAmount in loans)
            {
                var item = lendedAmount.Apply((borrowed - allocated));
                item.SetBlendedFactor(borrowed);
                items.Add(item);
                allocated += item.Amount;
            }
            
            // blended interest rate will be the sum up of all calculated factors per loan amount and rate.
            var interestRate = items.Sum(x => x.BlendedFactor);

            return interestRate;
        }

        public decimal CalculateAmountDue(decimal borrowed, int years, int yearlyFractions)
        {
            var rate = Math.Round(this.CalculateBlendedRate(borrowed),3);
            var rateCompound = Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + rate / yearlyFractions), years * yearlyFractions));
            var amount = borrowed * Math.Round(rateCompound,3);

            return amount;
        }

        public Quote GetQuote(decimal requestedAmount, int years = 3, int yearFractions = 12)
        {
            Quote quote;
            try
            {
                var rate = this.CalculateBlendedRate(requestedAmount);
                var dueAmount = this.CalculateAmountDue(requestedAmount, years, yearFractions);
                var monthlyDueAmount = dueAmount / (years * yearFractions);
                quote = new Quote(requestedAmount, rate, monthlyDueAmount, dueAmount);

            }
            catch (Exception ex)
            {
                quote = new Quote().Unable(ex);
            }

            
            return quote;
        }
    }
}
