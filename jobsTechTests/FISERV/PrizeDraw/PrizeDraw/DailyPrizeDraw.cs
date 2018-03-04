using System;
using System.Collections.Generic;
using System.Linq;

namespace PrizeDraw
{
    public class DailyPrizeDraw
    {

        private readonly string _input;
        private DailyPrizeDraw _previousDay = null;

        public DailyPrizeDraw(string input)
        {
            _input = input;
        }

        public DailyPrizeDraw(string input, DailyPrizeDraw previousDay)
        {
            _input = input;
            _previousDay = previousDay;
        }
        
        public IEnumerable<int> Items => GetItems();
        public IEnumerable<int> Remaining => GetRemainingOrders();
        public int OrderCount => GetRegisteredNumberOrders();
        public int CalculatePrizeAmount() 
        {
            var items = GetItems();
            var maxItem = items.Max();
            var minItem = items.Min();
            return maxItem - minItem;
        }

        private IEnumerable<int> GetRemainingOrders()
        {
            var items = GetItems();
            var maxItem = items.Max();
            var minItem = items.Min();
            var litems = items.ToList();

            litems.Remove(litems.First(x => x == maxItem));
            litems.Remove(litems.First(x => x == minItem));

            foreach (var item in litems)
            {
                yield return item;
            }

        }
        private int GetRegisteredNumberOrders()
        {
            int count = 0;

            int.TryParse(_input.Split()[0], out count);
            
            return count;
        }
        private IEnumerable<int> GetItems()
        {
            var items = _input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var regOrders = GetRegisteredNumberOrders();

            //0 <= r <= 100000
            if(regOrders < 0 || regOrders > 100000 || items.Length > 100000)
            {
                throw new Exception("number of registered daily orders is invalid: 0 <= r <= 100000.");
            }
            
            for ( int i = 1 ; i < items.Length ; i++)
            {
                yield return int.Parse(items[i]);
            }

            if (_previousDay != null)
            {
                foreach (var item in _previousDay.Remaining)
                {
                    yield return item;
                }
            }
        }

    }
}
