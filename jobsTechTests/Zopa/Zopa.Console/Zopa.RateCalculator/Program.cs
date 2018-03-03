using System;

namespace Zopa.RateCalculator
{
    class Program
    {
      
        static void Main(string[] args)
        {
            var file = args[0];
            var requestedAmount = Convert.ToDecimal(args[1]);
            var dataProvider = new MarketDataProvider(file);
            dataProvider.Load();
            var calculator = new Calculator(dataProvider);
            var quote = calculator.GetQuote(requestedAmount);
            Console.Write(quote.Print());
        }
    }
}
