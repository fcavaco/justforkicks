using System;

public static class LargestTwo
{
    internal class AgeCalculator
    {
        private readonly Int32[] _ages;
        internal AgeCalculator(Int32[] ages)
        {
            _ages = ages;
        }
        internal Int32[] TwoOldestAges()
        {
            int lenght = _ages.Length;
            int[] ages = new int[lenght];
            Array.Copy(_ages,0,ages,0,lenght);
            Array.Sort<Int32>(ages);
            Array.Reverse(ages);
            int[] result = { ages[1] , ages[0] };
            
            return result;
        }
    }

    public static int[] TwoOldestAges(int[] ages)
    {
        var calculator = new AgeCalculator(ages);
        return calculator.TwoOldestAges();
    }


}
