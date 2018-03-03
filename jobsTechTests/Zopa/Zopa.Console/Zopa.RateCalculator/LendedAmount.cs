using System;

namespace Zopa.RateCalculator
{
    [Serializable]
    public class LendedAmount
    {
        private decimal _blendedFactor;
        public LendedAmount() { }
        public LendedAmount(string lender, decimal rate, decimal amount)
        {
            Rate = rate;
            Lender = lender;
            Amount = amount;
        }

        public string Lender { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal BlendedFactor => _blendedFactor;

        public LendedAmount Apply(Decimal value)
        {
            if (value < this.Amount)
            {
                return new LendedAmount(this.Lender,this.Rate,value);
            }

            return new LendedAmount(this.Lender,this.Rate,this.Amount);
        }

        public void SetBlendedFactor(decimal borrowed)
        {
            _blendedFactor = (this.Amount / borrowed) * this.Rate; ;
        }
    }
}
