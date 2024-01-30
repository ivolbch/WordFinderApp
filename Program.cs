namespace WordFinderApp
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        static void Main()
        {
            var matrix = GetMatrix();
            var wordFinder = new WordFinder(matrix);

            var wordStream = GetWordStream();
            var result = wordFinder.Find(wordStream);

            Console.WriteLine("-------");
            Console.WriteLine("Matrix:");
            Console.WriteLine("-------");
            wordFinder.PrintMatrix();
            Console.WriteLine("");

            Console.WriteLine("-----------");
            Console.WriteLine("WordStream:");
            Console.WriteLine("-----------");
            foreach (var word in wordStream)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Top 10 words OrderByDescending:");
            Console.WriteLine("-------------------------------");
            foreach (var word in result)
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();
        }

        private static List<string> GetMatrix()
        {
            return new List<string>
            {
                "wbcdcabcdccold",
                "igwiofgwiocold",
                "chillchillcold",
                "salsalsnaaaaaa",
                "uvdxyuvdxyaaaa"
            };
        }

        private static List<string> GetWordStream()
        {
            return new List<string>
            {
                "chill",
                "cold",
                "wind",
                "salsa"
            };
        }
    }
}