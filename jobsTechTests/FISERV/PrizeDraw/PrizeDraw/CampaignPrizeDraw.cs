using System;

namespace PrizeDraw
{
    public class CampaignPrizeDraw
    {
        private readonly string[] _input;
        
        public CampaignPrizeDraw(string[] input)
        {
            _input = input;
        }

        public int CalculatePrizeAmount()
        {
            int result = 0;
            int numberOfOrders = 0;
            int numberOfDays = 0;
            int.TryParse(_input[0], out numberOfDays);
            //positive integer d(1 <= d <= 5000).
            if(numberOfDays < 1 || numberOfDays > 5000 || _input.Length < 1 || _input.Length > 5000)
            {
                throw new Exception("registered number of days is not allowed. positive integer d such that 1 <= d <= 5000.");
            }

            if (numberOfDays > 0)
            {
                DailyPrizeDraw previous = null;
                DailyPrizeDraw current = null;
                for (int i = 1 ; i < _input.Length; i++)
                {
                    if (i > 1)
                    {
                        current = new DailyPrizeDraw(_input[i], previous);
                    }
                    else
                    {
                        current = new DailyPrizeDraw(_input[i]);
                    }
                    numberOfOrders += current.OrderCount;
                    if( numberOfOrders > 100000 )
                    {
                        throw new Exception("number of orders exceeds campaign limit of 100000.");
                    }
                    previous = current;
                    result += current.CalculatePrizeAmount();
                }
            }
            return result;
        }

    }
}
