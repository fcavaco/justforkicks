using System;
using System.Text;
using System.Collections;
using System.Linq;

namespace CodeWars.Kata.ComplementaryDNA
{
    // best voted
    public class DnaStrand1
    {
        public static string MakeComplement(string dna)
        {
            return string.Concat(dna.Select(GetComplement));
        }

        public static char GetComplement(char symbol)
        {
            switch (symbol)
            {
                case 'A':
                    return 'T';
                case 'T':
                    return 'A';
                case 'C':
                    return 'G';
                case 'G':
                    return 'C';
                default:
                    throw new ArgumentException();
            }
        }
    }
    
    // mine
    public class DnaStrand 
    {
        public static string MakeComplement(string dna)
        {
            //Your code
            
            return Solution1(dna);
        }

        private static string Solution1(string dna){
            
            var mapper = new[] {
                ('A','T'),
                ('T','A'),
                ('C','G'),
                ('G','C'),
            };

            var builder = new StringBuilder();
            for(int i = 0; i < dna.Length; i++){
                Char actual = dna[i];
                Char complementary = mapper.First( x => x.Item1 == actual).Item2;
                builder.Append(complementary);
            }
            return builder.ToString();
        }
    }
}
