using System;
using System.IO;
using System.Linq;

namespace csharp_exercise
{
    class Day6
    {
        public static void LightGrid()
        {
            var lights = new int[1000,1000];

            var file = new StreamReader(@"santasproblems/day6_input.txt");
            var directions = string.Empty;

            while ((directions = file.ReadLine()) != null)
            {

                var santaCommand = ParseDirections(directions);

                if (santaCommand.Action.Equals("turnon"))
                    lights = TurnOnLights(santaCommand, lights);

                if (santaCommand.Action.Equals("turnoff"))
                    lights = TurnOffLights(santaCommand, lights);

                if (santaCommand.Action.Equals("toggle"))
                    lights = ToggleLights(santaCommand, lights);

            }

            var count = 0;

            for (var x = 0; x < 1000; x++)
            {
                for (var y = 0; y < 1000; y++)
                {
                    if (lights[x, y] == 1)
                        count++;
                }
            }

            Console.WriteLine("There are a total of " + count + " lights lit.");

            Program.Main();
        }

        private static int[,] TurnOnLights(SantaCommand input, int[,] lights)
        {
            for (var x = input.InitialX; x <= input.FinalX; x++)
            {
                for (var y = input.InitialY; y <= input.FinalY; y++)
                {
                    lights[x, y] = 1;
                }
            }

            return lights;
        }

        private static int[,] TurnOffLights(SantaCommand input, int[,] lights)
        {
            for (var x = input.InitialX; x <= input.FinalX; x++)
            {
                for (var y = input.InitialY; y <= input.FinalY; y++)
                {
                    lights[x, y] = 0;
                }
            }

            return lights;
        }

        private static int[,] ToggleLights(SantaCommand input, int[,] lights)
        {

            for (var x = input.InitialX; x <= input.FinalX; x++)
            {
                for (var y = input.InitialY; y <= input.FinalY; y++)
                {
                    if (lights[x, y] == 0)
                        lights[x, y] = 1;
                    else
                        lights[x, y] = 0;
                }
            }

            return lights;
        }

        private static SantaCommand ParseDirections(string input)
        {
            var values = input.Split(' ').ToList();

            if (values[0].Equals("turn"))
            {
                values[0] = values[0] + values[1];
                values.Remove(values[1]);
            }

            var explicitDirections = new SantaCommand
            {
                Action = values[0],
                InitialX = Convert.ToInt32(values[1].Split(',')[0]),
                InitialY = Convert.ToInt32(values[1].Split(',')[1]),
                FinalX = Convert.ToInt32(values[3].Split(',')[0]),
                FinalY = Convert.ToInt32(values[3].Split(',')[1]),
            };

            return explicitDirections;
        }

        private class SantaCommand
        {
            public string Action { get; set; }
            public int InitialX { get; set; }
            public int InitialY { get; set; }
            public int FinalX { get; set; }
            public int FinalY { get; set; }
        }
    }
}
