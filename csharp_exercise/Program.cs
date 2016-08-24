using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace csharp_exercise
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Please choose a utility to execute below");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("0. AreAnagrams");
            Console.WriteLine("1. SantasApartment");
            Console.WriteLine("2. WrappingPaper");
            Console.WriteLine("3. HouseDelivery");
            Console.WriteLine("4. AdventCoins");
            Console.WriteLine("5. NaughtyOrNiceStrings");
            Console.WriteLine("6. LightGrid");
            Console.WriteLine("7. BitwiseLogicGates");
            Console.WriteLine("8. LiteralVsMemoryString");
            Console.WriteLine("\nEnter any other key to exit...\n\n");
            int userInput;

            if (int.TryParse(Console.ReadLine(), out userInput))
            {
                switch (userInput)
                {
                    case 0:
                        // Are 2 words an anagram?
                        AreAnagrams();
                        break;
                    case 1:
                        // Santa is trying to deliver presents in a large apartment building, but he can't find the right floor -
                        // the directions he got are a little confusing. He starts on the ground floor (floor 0) and then follows
                        // the instructions one character at a time. An opening parenthesis, (, means he should go up one floor,
                        // and a closing parenthesis, ), means he should go down one floor. The apartment building is very tall,
                        // and the basement is very deep; he will never find the top or bottom floors.
                        SantasApartment();
                        break;
                    case 2:
                        // The elves are running low on wrapping paper, and so they need to submit an order for more.They have a 
                        // list of the dimensions(length l, width w, and height h) of each present, and only want to order exactly as 
                        // much as they need. Fortunately, every present is a box(a perfect right rectangular prism), which makes 
                        // calculating the required wrapping paper for each gift a little easier: find the surface area of the box, 
                        // which is 2*l*w + 2*w*h + 2*h*l.The elves also need a little extra paper for each present: the area of the
                        // smallest side.
                        WrappingPaper();
                        break;
                    case 3:
                        // Santa is delivering presents to an infinite two - dimensional grid of houses. He begins by delivering a 
                        // present to the house at his starting location, and then an elf at the North Pole calls him via radio and 
                        // tells him where to move next.Moves are always exactly one house to the north(^), south(v), east(>), or west 
                        // (<).After each move, he delivers another present to the house at his new location. However, the elf back 
                        // at the north pole has had a little too much eggnog, and so his directions are a little off, and Santa ends 
                        // up visiting some houses more than once.How many houses receive at least one present?
                        HouseDelivery();
                        break;
                    case 4:
                        // Santa needs help mining some AdventCoins (very similar to bitcoins) to use as gifts for all the economically 
                        // forward -thinking little girls and boys. To do this, he needs to find MD5 hashes which, in hexadecimal, 
                        // start with at least five zeroes. The input to the MD5 hash is some secret key (your puzzle input, given below) 
                        // followed by a number in decimal.To mine AdventCoins, you must find Santa the lowest positive number(no leading 
                        // zeroes: 1, 2, 3, ...) that produces such a hash.
                        AdventCoins();
                        break;
                    case 5:
                        // Santa needs help figuring out which strings in his text file are naughty or nice. A nice string is one with all
                        // of the following properties: It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
                        // It contains at least one letter that appears twice in a row, like xx, abcdde(dd), or aabbccdd (aa, bb, cc, or dd).
                        // It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
                        Day5.NaughtyOrNiceStrings();
                        break;
                    case 6:
                        // Because your neighbors keep defeating you in the holiday house decorating contest year after year, you've decided 
                        // to deploy one million lights in a 1000x1000 grid. Furthermore, because you've been especially nice this year, Santa 
                        // has mailed you instructions on how to display the ideal lighting configuration. Lights in your grid are numbered 
                        // from 0 to 999 in each direction; the lights at each corner are at 0,0, 0,999, 999,999, and 999,0.The instructions 
                        // include whether to turn on, turn off, or toggle various inclusive ranges given as coordinate pairs.Each coordinate 
                        // pair represents opposite corners of a rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers 
                        // to 9 lights in a 3x3 square. The lights all start turned off. To defeat your neighbors this year, all you have to 
                        // do is set up your lights by doing the instructions Santa sent you in order.
                        Day6.LightGrid();
                        break;
                    case 7:
                        // This year, Santa brought little Bobby Tables a set of wires and bitwise logic gates! Unfortunately, little Bobby is 
                        // a little under the recommended age range, and he needs help assembling the circuit. Each wire has an identifier 
                        // (some lowercase letters) and can carry a 16 - bit signal(a number from 0 to 65535).A signal is provided to each 
                        // wire by a gate, another wire, or some specific value. Each wire can only get a signal from one source, but can 
                        // provide its signal to multiple destinations.A gate provides no signal until all of its inputs have a signal.
                        Day7.BitwiseLogicGates();
                        break;
                    case 8:
                        // Space on the sleigh is limited this year, and so Santa will be bringing his list as a digital copy. He needs to 
                        // know how much space it will take up when stored.
                        //
                        // Santa's list is a file that contains many double-quoted string literals, one on each line. The only escape sequences 
                        // used are \\ (which represents a single backslash), \" (which represents a lone double-quote character), and \x plus 
                        // two hexadecimal characters (which represents a single character with that ASCII code). Disregarding the whitespace 
                        // in the file, what is the number of characters of code for string literals minus the number of characters in memory 
                        // for the values of the strings in total for the entire file ?
                        Day8.LiteralVsMemoryString(); //TODO: Does not work
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }

            Environment.Exit(0);
        }

        public static void AreAnagrams()
        {
            var words = new string[2];
            var firstWordTotal = 0;
            var secondWordTotal = 0;

            Console.Write("Please enter the first word: ");
            words[0] = Console.ReadLine();

            Console.Write("Please enter the second word: ");
            words[1] = Console.ReadLine();

            foreach (var word in words)
            {
                var asciiBytes = Encoding.ASCII.GetBytes(word);
                var total = 0;

                foreach (var asciiByte in asciiBytes)
                {
                    total += asciiByte;

                    if (word == words[0]) firstWordTotal = total;
                    if (word == words[1]) secondWordTotal = total;
                }
            }

            Console.WriteLine(firstWordTotal == secondWordTotal ? "These are anagrams!" : "These aren't anagrams :(");

            Main();
        }

        public static void SantasApartment()
        {
            var floor = 0;
            var directions = string.Empty;
            var basementActivity = false;
            var position = 0;

            Console.WriteLine("Evaluating floors...");

            var file = new StreamReader(@"santasproblems/day1_input.txt");

            while ((directions = file.ReadLine()) != null)
            {
                foreach (var direction in directions)
                {
                    position++;

                    if (direction == '(')
                        floor += 1;
                    else if (direction == ')')
                        floor -= 1;

                    // Now, given the same instructions, find the position of the first character that causes him to enter the basement 
                    // (floor -1). The first character in the instructions has position 1, the second character has position 2, and so on.
                    if (floor == -1 && !basementActivity)
                    {
                        Console.WriteLine("Santa entered the basement on direction " + position);
                        basementActivity = true;
                    }
                }
            }

            Console.WriteLine("Santa should be on floor " + floor);

            Main();
        }

        public static void WrappingPaper()
        {
            var totalPaperNeeded = 0;
            var totalRibbonNeeded = 0;
            var packageDimensions = string.Empty;

            Console.WriteLine("Evaluating paper...");

            var file = new StreamReader(@"santasproblems/day2_input.txt");

            while ((packageDimensions = file.ReadLine()) != null)
            {
                // 29x13x26

                var length = Convert.ToInt32(packageDimensions.Split('x')[0]);
                var width = Convert.ToInt32(packageDimensions.Split('x')[1]);
                var height = Convert.ToInt32(packageDimensions.Split('x')[2]);

                var sides = new List<int>
                {
                    length,
                    width,
                    height
                };

                var calcs = new List<int>
                {
                    (2*length*width),
                    (2*width*height),
                    (2*height*length)
                };

                var lowest = calcs.Min() / 2;
                var surfacearea = calcs.Sum() + lowest;

                totalPaperNeeded += surfacearea;

                // The elves are also running low on ribbon. Ribbon is all the same width, so they only have to worry about the length 
                // they need to order, which they would again like to be exact. The ribbon required to wrap a present is the shortest 
                // distance around its sides, or the smallest perimeter of any one face. Each present also requires a bow made out of 
                // ribbon as well; the feet of ribbon required for the perfect bow is equal to the cubic feet of volume of the present.
                // Don't ask how they tie the bow, though; they'll never tell.
                sides.Remove(sides.Max());

                var ribbonfeet = (sides[0] * 2) + (sides[1] * 2);
                var bowfeet = length * width * height;

                totalRibbonNeeded += ribbonfeet + bowfeet;
            }

            Console.WriteLine("The elves need " + totalPaperNeeded + " square feet of paper and " + totalRibbonNeeded + " feet of ribbon.");

            Main();
        }

        public static void HouseDelivery()
        {
            //TODO: why is this wrong
            var directions = string.Empty;
            var housesVisited = new List<Tuple<int, int>>();
            var roboHousesVisited = new List<Tuple<int, int>>();
            var santasdirection = true;

            Console.WriteLine("Evaluating directions...");

            var file = new StreamReader(@"santasproblems/day3_input.txt");

            while ((directions = file.ReadLine()) != null)
            {
                var x_coord = 0;
                var y_coord = 0;
                var robo_x_coord = 0;
                var robo_y_coord = 0;

                foreach (var direction in directions)
                {
                    if (santasdirection)
                    {
                        if (direction == '^')
                            y_coord += 1;
                        if (direction == 'v')
                            y_coord -= 1;
                        if (direction == '>')
                            x_coord += 1;
                        if (direction == '<')
                            x_coord -= 1;

                        var houseLocation = Tuple.Create(x_coord, y_coord);

                        housesVisited.Add(houseLocation);
                    }

                    if (!santasdirection)
                    {
                        if (direction == '^')
                            robo_y_coord += 1;
                        if (direction == 'v')
                            robo_y_coord -= 1;
                        if (direction == '>')
                            robo_x_coord += 1;
                        if (direction == '<')
                            robo_x_coord -= 1;

                        var houseLocation = Tuple.Create(robo_x_coord, robo_y_coord);

                        roboHousesVisited.Add(houseLocation);
                    }

                    santasdirection = !santasdirection;
                }
            }

            // remove any times the starting point is revisited
            housesVisited.RemoveAll(item => item.Item1 == 0 && item.Item2 == 0);
            roboHousesVisited.RemoveAll(item => item.Item1 == 0 && item.Item2 == 0);

            var minimumHousesWithPresents = housesVisited.Distinct().Count() + roboHousesVisited.Distinct().Count() + 1; // add 1 for initial starting point of both santas

            Console.WriteLine("Santa had delivered at least one present to " + minimumHousesWithPresents + " houses");

            Main();
        }

        public static void AdventCoins()
        {
            var md5 = MD5.Create();
            var input = "";

            var possiblehash = "";
            var possibleNumbers = 0;

            do
            {
                input = "ckczppom";
                input += possibleNumbers.ToString();

                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hash = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();

                foreach (byte t in hash)
                {
                    sb.Append(t.ToString("X2"));
                }

                possiblehash = sb.ToString().Substring(0, 5);
                possibleNumbers++;

            } while (possiblehash != "00000");

            Console.WriteLine(input);

            Main();
        }

        //public static void StringReverse()
        //{
        //    var value = "ABC";
        //    var stringvalue = "";
        //    var length = value.Length - 1;

        //    do
        //    {
        //        stringvalue += value[length];
        //        length--;
        //    } while (length > -1);

        //    Console.WriteLine(stringvalue);

        //    Main();
        //}


    }
}