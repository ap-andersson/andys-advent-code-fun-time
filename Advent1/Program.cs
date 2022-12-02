using System.Diagnostics;

namespace Advent1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting elf calorie counting and stopwatch!");

            var sw = new Stopwatch();
            sw.Start();

            // Key is elf number
            // Value is linked list of all cal numbers for that elf
            var calVals = new Dictionary<int, LinkedList<int>>
            {
                { 1, new LinkedList<int>() }
            };

            // Assume we should think "normally", not as a developer. Hence 1 is first, not 0.
            int elfCounter = 1;

            // Read all lines and add all calorie-numbers for each elf
            foreach(var line in File.ReadLines("input.txt"))
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    elfCounter++;
                    calVals.Add(elfCounter, new LinkedList<int>());
                    continue;
                }

                calVals[elfCounter].AddLast(int.Parse(line));
            }

            // Sum all the calories for each elf
            var totalCalsByElf = calVals
                .ToDictionary(
                    x => x.Key, 
                    x => x.Value.Sum()
                    )
                .OrderByDescending(x => x.Value)
                .ToList();

            // For funz and verification...
            //foreach(var totalCal in totalCalsByElf)
            //{
            //    Console.WriteLine($"Elf nr '{totalCal.Key}' has '{totalCal.Value}' calories");
            //}

            // Find the elf with most calories
            var elfWithMostCaloriesWithTotal = totalCalsByElf.First();

            Console.WriteLine($"Elf nr '{elfWithMostCaloriesWithTotal.Key}' has most calories, which is '{elfWithMostCaloriesWithTotal.Value}'");


            // Part 3
            var threeTopElves = totalCalsByElf.Take(3).ToList();

            var totalCalsForTopThree = threeTopElves.Select(x => x.Value).Sum();

            Console.WriteLine($"Top three elves is {string.Join(",", threeTopElves.Select(x => x.Key))} and have '{totalCalsForTopThree}' calories in total");

            sw.Stop();

            Console.WriteLine($"Execution took {sw.Elapsed.TotalMilliseconds:00.0}ms");

            Console.ReadKey();
        }
    }

}