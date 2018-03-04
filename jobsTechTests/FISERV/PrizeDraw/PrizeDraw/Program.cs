using System;
using System.IO;

namespace PrizeDraw
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = "";

            if(args!=null && args.Length > 0)
            {
                content = File.ReadAllText(args[0]);
            }
            if (Console.IsInputRedirected)
            {
                TextReader reader = Console.In;
                content = reader.ReadToEnd();
            };

            var input = content.Split("\r\n",StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (input.Length > 0)
                {
                    var draw = new CampaignPrizeDraw(input);
                    var amount = draw.CalculatePrizeAmount();
                    Console.WriteLine(amount);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
