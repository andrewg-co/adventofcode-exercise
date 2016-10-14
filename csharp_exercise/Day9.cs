using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csharp_exercise
{
    class Day9
    {
        public static void DistanceCalc()
        {
            string directions;
            var locationlist = new List<string[]>();

            var file = new StreamReader(@"santasproblems/day9_input.txt");
            while ((directions = file.ReadLine()) != null)
            {
                var locations = directions.Split(' ');
                locationlist.Add(locations);
            }

            locationlist.Sort();

            Program.Main();
        }

        private static IEnumerable<string[]> GetPermutations(string[] list, int length)
        {
            return length == 1
                ? list.Select(t => new[] { t })
                : GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new[] { t2 }).ToArray());
        }
    }
}
