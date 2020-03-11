using System;
using System.Linq;

namespace CodeWars.Kata.TakeTenMinuteWalk
{
    public class Kata
    {
        public static bool IsValidWalk(string[] walk)
        {
            if(walk.Length == 10) 
            {
                var north = walk.Count(x => x.Equals("n"));
                var south = walk.Count(x => x.Equals("s"));
                var west = walk.Count(x => x.Equals("w"));
                var east = walk.Count(x => x.Equals("e"));

                // moves up&down and left&right need to match-up , so that position is returned back to beginning.
                if(north == south && west == east)
                    return true;
            }

            return false;
        }
    }
}
