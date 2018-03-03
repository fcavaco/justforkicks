using CsvHelper.Configuration;

namespace Zopa.RateCalculator
{
    public sealed class LendedAmountMap : ClassMap<LendedAmount>
    {
        public LendedAmountMap()
        {
            Map(x => x.Amount).Name("Available");
            Map(x => x.Lender).Name("Lender");
            Map(x => x.Rate).Name("Rate");

        }
    }
}