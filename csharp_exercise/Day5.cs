using System;
using System.IO;
using System.Linq;

namespace csharp_exercise
{
    class Day5
    {
        public static void NaughtyOrNiceStrings()
        {
            var file = new StreamReader(@"santasproblems/day5_input.txt");
            var evalstring = string.Empty;
            var niceStrings = 0;

            while ((evalstring = file.ReadLine()) != null)
            {
                if (CombinationExceptions(evalstring) && HasDoubleLetter(evalstring) && VowelCount(evalstring))
                    niceStrings++;
            }

            Console.WriteLine("Santa has " + niceStrings + " nice strings");

            Program.Main();
        }

        private static bool CombinationExceptions(string input)
        {
            if (input.Contains("ab") ||
                input.Contains("cd") ||
                input.Contains("pq") ||
                input.Contains("xy"))

                return false;

            return true;
        }

        private static bool HasDoubleLetter(string input)
        {
            var index = 1;

            foreach (var character in input)
            {
                if (index == input.Length) return false;

                var nextChar = input[index];

                if (nextChar == character) return true;

                index++;
            }

            return false;
        }

        private static bool VowelCount(string input)
        {
            var vowelAmount = input.Count(character => character == 'a' || character == 'e' || character == 'i' || character == 'o' || character == 'u');

            return vowelAmount >= 3;
        }
    }
}
