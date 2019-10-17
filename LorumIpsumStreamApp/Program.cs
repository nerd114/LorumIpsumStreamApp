using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LorumIpsum;

namespace LorumIpsumStreamApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LpStream lpStream = new LpStream();

            // change this to how many words to read before showing stats
            // alternatively, we could pass this as an arg
            int words = 100;
            while (true)
            {
                lpStream.ReadText(words);
                Console.WriteLine("[Entire Read]: " + lpStream.Text);
                Console.WriteLine("[Last " + words + " Word(s) Read]: " + lpStream.LastReadText);
                Console.WriteLine("[Characters]: " + lpStream.Characters);
                Console.WriteLine("[Words]: " + lpStream.Words);
                Console.WriteLine("");
                if (lpStream.Largest5 != null && lpStream.Largest5.Count > 0)
                {
                    Console.WriteLine("[<< 5 Largest Words >>]");
                    foreach (Word word in lpStream.Largest5)
                    {
                        Console.WriteLine(word.Value);
                    }
                }
                Console.WriteLine("");
                if (lpStream.Smallest5 != null && lpStream.Smallest5.Count > 0)
                {
                    Console.WriteLine("[<< 5 Smallest Words >>]");
                    foreach (Word word in lpStream.Smallest5)
                    {
                        Console.WriteLine(word.Value);
                    }
                }
                Console.WriteLine("");
                if (lpStream.FrequentlyUsed10 != null && lpStream.FrequentlyUsed10.Count > 0)
                {
                    Console.WriteLine("[<< 10 Most Frequent Words >>]");
                    foreach (Word word in lpStream.FrequentlyUsed10)
                    {
                        Console.WriteLine(word.Value + " (x" + word.Frequency + ")");
                    }
                }
                Console.WriteLine("");
                if (lpStream.AllCharacters != null && lpStream.AllCharacters.Count > 0)
                {
                    Console.WriteLine("[<< All Characters Used >>]");
                    foreach (Character character in lpStream.AllCharacters)
                    {
                        Console.WriteLine(character.Value + " (x" + character.Frequency + ")");
                    }
                }
                Console.WriteLine("Press Escape to stop. Any key to continue...");
                Console.WriteLine("");
                ConsoleKeyInfo result = Console.ReadKey();
                if (result.Key == ConsoleKey.Escape) break;
            }

            lpStream.Dispose();

        }
    }
}
