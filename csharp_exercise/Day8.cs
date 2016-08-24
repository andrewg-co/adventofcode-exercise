using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace csharp_exercise
{
    class Day8
    {
        public static void LiteralVsMemoryString()
        {
            string directions;
            var stringLiteralCount = 0;
            var stringMemoryCount = 0;

            var file = new StreamReader(@"santasproblems/day8_input.txt");
            while ((directions = file.ReadLine()) != null)
            {
                stringMemoryCount += directions.Length;
                directions = directions.Remove(directions.Length - 1, 1);
                directions = directions.Remove(0, 1);

                var regex = new Regex(@"\\x..");
                var matches = regex.Matches(directions);

                if (matches.Count != 0)
                {
                    foreach (Match match in matches)
                    {
                        foreach (Capture capture in match.Captures)
                        {
                            directions = directions.Replace(capture.Value, "");
                            stringLiteralCount++;
                        }
                    }
                }

                regex = new Regex(@"\\\""");
                matches = regex.Matches(directions);

                if (matches.Count != 0)
                {
                    foreach (Match match in matches)
                    {
                        foreach (Capture capture in match.Captures)
                        {
                            directions = directions.Replace(capture.Value, "");
                            stringLiteralCount++;
                        }
                    }
                }

                regex = new Regex(@"\\\\");
                matches = regex.Matches(directions);

                if (matches.Count != 0)
                {
                    foreach (Match match in matches)
                    {
                        foreach (Capture capture in match.Captures)
                        {
                            directions = directions.Replace(capture.Value, "");
                            stringLiteralCount++;
                        }
                    }
                }

                stringLiteralCount += directions.Length;
            }

            Console.WriteLine("There are " + (stringMemoryCount - stringLiteralCount) + " characters of code difference");

            Program.Main();
        }
    }
}
