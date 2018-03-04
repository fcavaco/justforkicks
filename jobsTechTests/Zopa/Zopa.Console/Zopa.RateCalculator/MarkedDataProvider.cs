using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Zopa.RateCalculator
{
    public class MarketDataProvider
    {
        private readonly string _dataFile;
        public IList<LendedAmount> _availableLoans = new List<LendedAmount>();

        public MarketDataProvider(string file)
        {
            _dataFile = file;
        }
        public MarketDataProvider()
        {
        }

        public void Load()
        {
            var mapper = new LendedAmountMap();
            var items = FileHelper.LoadCsv<LendedAmountMap, LendedAmount>(mapper, _dataFile);
            foreach (var item in items )
            {
             _availableLoans.Add(item);
            }
        }
        public virtual IList<LendedAmount> AvailableLoans => _availableLoans;
    }
}