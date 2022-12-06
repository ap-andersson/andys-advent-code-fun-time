using System.Diagnostics;

namespace AdventOfCode.Day3
{
    public class Day3
    {
        

        public static void Run(string[] args)
        {

            Console.WriteLine("Starting elf calorie counting and stopwatch!");

            var sw = new Stopwatch();
            sw.Start();

            char[] relevantChars = new char[14];
            char[] buffer = new char[relevantChars.Length*4];

            int charNumber = 0;
            int charsProcessed = 0;
            using (var fileReader = File.OpenText("Day3/input3.txt"))
            {
                int charsRead;
                do
                {
                    charsRead = fileReader.Read(buffer, 0, buffer.Length);

                    for (int i = 0; i < charsRead; i++)
                    {
                        Array.Copy(relevantChars, 0, relevantChars, 1, relevantChars.Length - 1);

                        relevantChars[0] = buffer[i];

                        //Console.WriteLine($"Processed:{(charsProcessed + i + 1)}.");
                        
                        var charByCount = relevantChars
                            .GroupBy(x => x)
                            .ToDictionary(
                                x => x.Key, 
                                x => x.Count()
                                );

                        if ((charsProcessed > 0 || i >= relevantChars.Length) && charNumber == 0 && charByCount.Count == relevantChars.Length)
                        {
                            charNumber = (charsProcessed + i + 1);
                            //Console.WriteLine("Char number set to " + charNumber);
                            break;
                        }
;                    }

                    charsProcessed += charsRead;

                } while (charsRead > 0);


                Console.WriteLine($"After '{charNumber}' chars it was found");
                Console.WriteLine($"Total chars: '{charsProcessed}'");
            }

            // 929 too low
            // 1239 too low <-- This fucked me up
            // 1240 too low <-- This fucked me up even more

            sw.Stop();

            Console.WriteLine();

            Console.WriteLine($"Execution took {sw.Elapsed.TotalMilliseconds:0.}ms");

            Console.ReadKey();
        }
        
    }
}
