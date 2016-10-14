using System;

namespace csharp_exercise
{
    class Day10
    {
        public static void LookAndSay()
        {
            var puzzleInput = "1113122113";
            var currentCount = 1;
            var puzzleOutput = "";
            var puzzleCount = 0;

            do
            {
                for (var i = 0; i < puzzleInput.Length; i++)
                {
                    if (i == 0) continue;

                    if (puzzleInput[i] == puzzleInput[i - 1])
                    {
                        // if the current number matches the previous number, increment the count and continue the loop
                        currentCount++;
                    }
                    else
                    {
                        // if they do not match, take whatever is counted and create the new numbers
                        puzzleOutput += currentCount.ToString() + puzzleInput[i - 1];
                        // also, reset the currentCount to 0
                        currentCount = 1;
                    }
                }

                puzzleInput = puzzleOutput;
                puzzleCount++;
            } while (puzzleCount <= 40);

            Console.WriteLine("The length of the puzzle is: " + puzzleInput.Length);

            Program.Main();
        }
    }
}
