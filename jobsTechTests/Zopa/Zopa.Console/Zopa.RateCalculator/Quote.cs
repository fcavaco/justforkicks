using System;
using System.Text;

namespace Zopa.RateCalculator
{
    public struct Quote
    {
        private readonly decimal _requestedAmount;
        private readonly decimal _rate;
        private readonly decimal _monthlyRepayment;
        private readonly decimal _totalRepayment;
        private string _printInstead;
        public Quote(decimal requestedAmount, decimal rate, decimal monthlyRepayment, decimal totalRepayment)
        {
            _requestedAmount = requestedAmount;
            _monthlyRepayment = monthlyRepayment;
            _totalRepayment = totalRepayment;
            _rate = rate;
            _printInstead = "";
        }

        private decimal RequestedAmount => _requestedAmount;
        private decimal Rate => _rate;
        private decimal MonthlyRepayment => _monthlyRepayment;
        private decimal TotalRepayment => _totalRepayment;

        public string Print()
        {
            StringBuilder builder = new StringBuilder();
            if (!String.IsNullOrEmpty(_printInstead))
            {
                builder.AppendLine($"{_printInstead}");
            }
            else
            {
                builder.AppendLine($"Requested amount: £{Math.Round(this.RequestedAmount, 2)}");
                builder.AppendLine($"Rate: {Math.Round(this.Rate * 100, 1)}");
                builder.AppendLine($"Monthly repayment: £{Math.Round(this.MonthlyRepayment, 2)}");
                builder.AppendLine($"Total repayment: £{Math.Round(this.TotalRepayment, 2)}");
            }
            return builder.ToString();
        }

        
        public Quote Unable(Exception ex)
        {
            _printInstead = ex.Message;
            return this;
        }
    }
}