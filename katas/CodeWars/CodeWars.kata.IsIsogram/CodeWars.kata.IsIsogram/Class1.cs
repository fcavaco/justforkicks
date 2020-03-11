using System.Linq;

namespace CodeWars.kata.IsIsogram
{
    public class Kata
{
  public static bool IsIsogram(string str) 
  {
    // Code on you crazy diamond!
    var chars = str.ToLower().ToCharArray();
    var size = chars.Length;
    var withoutDups = chars.Distinct().ToArray();
    var size1 =  withoutDups.Length;
    
    return (size == size1);
  }
}
}
