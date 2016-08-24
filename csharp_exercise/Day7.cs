using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace csharp_exercise
{
    internal class Day7
    {
        public static void BitwiseLogicGates()
        {
            var wires = new Dictionary<string, UInt16>();
            string directions;

            do
            {
                var file = new StreamReader(@"santasproblems/day7_input.txt");
                while ((directions = file.ReadLine()) != null)
                {
                    var lefthand = directions.Split(new[] { "->" }, StringSplitOptions.None)[0].Trim().Split(' ');
                    var righthand = directions.Split(new[] { "->" }, StringSplitOptions.None)[1].Trim();

                    if (lefthand.Length == 1)
                        SimpleAssign(lefthand[0], righthand, wires);

                    if (lefthand.Length == 2)
                        BitwiseComplement(lefthand[1], righthand, wires);

                    if (lefthand.Length == 3)
                        ValueBitwiseValue(lefthand, righthand, wires);
                }
            } while (wires.ContainsValue(0)); //TODO: This will eventually get the right value to wire A, however not all wires created get a value. Need to fix

            Console.WriteLine("The value of wire A is: " + wires.FirstOrDefault(x => x.Key == "a"));

            Program.Main();
        }

        private static void SimpleAssign(string startingValue, string endingWire, Dictionary<string, UInt16> wires)
        {
            IfNewWireThenCreate(endingWire, wires);

            if (startingValue.All(char.IsDigit))
            {
                wires[endingWire] = Convert.ToUInt16(startingValue);
            }
            else
            {
                IfNewWireThenCreate(startingValue, wires);
                wires[endingWire] = wires[startingValue];
            }
        }

        private static void BitwiseComplement(string startingWire, string endingWire, Dictionary<string, UInt16> wires)
        {
            IfNewWireThenCreate(endingWire, wires);
            IfNewWireThenCreate(startingWire, wires);

            wires[endingWire] = (ushort)~wires[startingWire];
        } 

        private static void ValueBitwiseValue(string[] lefthand, string endingWire, Dictionary<string, UInt16> wires)
        {
            var startingValue = lefthand[0];
            var operation = lefthand[1];
            var endingValue = lefthand[2];

            if (startingValue.All(char.IsDigit) && !endingValue.All(char.IsDigit))
            {
                IfNewWireThenCreate(endingValue, wires);
                var startingValueNum = Convert.ToUInt16(startingValue);

                switch (operation)
                {
                    case "AND":
                        wires[endingWire] = (ushort) (startingValueNum & wires[endingValue]);
                        break;
                    case "LSHIFT":
                        wires[endingWire] = (ushort) (startingValueNum << wires[endingValue]);
                        break;
                    case "RSHIFT":
                        wires[endingWire] = (ushort) (startingValueNum >> wires[endingValue]);
                        break;
                    case "OR":
                        wires[endingWire] = (ushort) (startingValueNum | wires[endingValue]);
                        break;
                    default:
                        Console.WriteLine(operation + " is not an operation!");
                        break;
                }
            } else if (!startingValue.All(char.IsDigit) && endingValue.All(char.IsDigit))
            {
                IfNewWireThenCreate(startingValue, wires);
                var endingValueNum = Convert.ToUInt16(endingValue);

                switch (operation)
                {
                    case "AND":
                        wires[endingWire] = (ushort)(wires[startingValue] & endingValueNum);
                        break;
                    case "LSHIFT":
                        wires[endingWire] = (ushort)(wires[startingValue] << endingValueNum);
                        break;
                    case "RSHIFT":
                        wires[endingWire] = (ushort)(wires[startingValue] >> endingValueNum);
                        break;
                    case "OR":
                        wires[endingWire] = (ushort)(wires[startingValue] | endingValueNum);
                        break;
                    default:
                        Console.WriteLine(operation + " is not an operation!");
                        break;
                }
            } else if (!startingValue.All(char.IsDigit) && !endingValue.All(char.IsDigit))
            {
                IfNewWireThenCreate(startingValue, wires);
                IfNewWireThenCreate(endingValue, wires);

                switch (operation)
                {
                    case "AND":
                        wires[endingWire] = (ushort)(wires[startingValue] & wires[endingValue]);
                        break;
                    case "LSHIFT":
                        wires[endingWire] = (ushort)(wires[startingValue] << wires[endingValue]);
                        break;
                    case "RSHIFT":
                        wires[endingWire] = (ushort)(wires[startingValue] >> wires[endingValue]);
                        break;
                    case "OR":
                        wires[endingWire] = (ushort)(wires[startingValue] | wires[endingValue]);
                        break;
                    default:
                        Console.WriteLine(operation + " is not an operation!");
                        break;
                }
            }
        }

        private static void IfNewWireThenCreate(string result, Dictionary<string, UInt16> wires)
        {
            if(!wires.ContainsKey(result))
                wires.Add(result, Convert.ToUInt16(0));
        }
    }
}
