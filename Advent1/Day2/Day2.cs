using System.Diagnostics;

namespace AdventOfCode.Day2
{
    public class Day2
    {
        private static readonly Dictionary<GameOption, int> ScoresByOption = new Dictionary<GameOption, int>
        {
            { GameOption.Rock, 1 },
            { GameOption.Paper, 2 },
            { GameOption.Scissor, 3 },
        };

        // Dictionary<TheirChoice, Dictionary<MyChoice, MyScore>>
        private static readonly Dictionary<GameOption, Dictionary<GameOption, int>> ScoreMatrix =
            new Dictionary<GameOption, Dictionary<GameOption, int>>
            {
                { 
                    GameOption.Rock, new Dictionary<GameOption, int>
                    {
                        { GameOption.Rock, 3+ScoresByOption[GameOption.Rock] },
                        { GameOption.Paper, 6+ScoresByOption[GameOption.Paper] },
                        { GameOption.Scissor, 0+ScoresByOption[GameOption.Scissor] }
                    }
                },
                {
                    GameOption.Paper, new Dictionary<GameOption, int>
                    {
                        { GameOption.Rock, 0+ScoresByOption[GameOption.Rock] },
                        { GameOption.Paper, 3+ScoresByOption[GameOption.Paper] },
                        { GameOption.Scissor, 6+ScoresByOption[GameOption.Scissor] }
                    }
                },
                {
                    GameOption.Scissor, new Dictionary<GameOption, int>
                    {
                        { GameOption.Rock, 6+ScoresByOption[GameOption.Rock] },
                        { GameOption.Paper, 0+ScoresByOption[GameOption.Paper] },
                        { GameOption.Scissor, 3+ScoresByOption[GameOption.Scissor] }
                    }
                }
            };

        // Dictionary<TheirChoice, Dictionary<DesiredOutcome, MyChoice>>
        private static readonly Dictionary<GameOption, Dictionary<GameResult, GameOption>> ChoiceMatrix =
            new Dictionary<GameOption, Dictionary<GameResult, GameOption>>
            {
                {
                    GameOption.Rock, new Dictionary<GameResult, GameOption>
                    {
                        { GameResult.Lose, GameOption.Scissor },
                        { GameResult.Draw, GameOption.Rock },
                        { GameResult.Win, GameOption.Paper }
                    }
                },
                {
                    GameOption.Paper, new Dictionary<GameResult, GameOption>
                    {
                        { GameResult.Lose, GameOption.Rock },
                        { GameResult.Draw, GameOption.Paper },
                        { GameResult.Win, GameOption.Scissor }
                    }
                },
                {
                    GameOption.Scissor, new Dictionary<GameResult, GameOption>
                    {
                        { GameResult.Lose, GameOption.Paper },
                        { GameResult.Draw, GameOption.Scissor },
                        { GameResult.Win, GameOption.Rock }
                    }
                }
            };

        private enum GameOption
        {
            Rock = 0, // Opponent = A, You = X
            Paper = 1, // Opponent = B, You = Y
            Scissor = 2, // Opponent = C, You = Z
        }

        private enum GameResult
        {
            Lose = 0, // = X
            Draw = 1, // = Y
            Win = 2, // = Z
        }

        public static void Run(string[] args)
        {

            Console.WriteLine("Starting elf calorie counting and stopwatch!");

            var sw = new Stopwatch();
            sw.Start();

            int rowCount = 1;
            var rowGameChoices = new Dictionary<int, (GameOption opponentsChoice, GameOption yourChoice)>();
            var rowGameResults = new Dictionary<int, (GameOption opponentsChoice, GameResult desiredResult)>();
            using (var fileReader = File.OpenText("Day2/input2.txt"))
            {
                do
                {
                    var currentLine = fileReader.ReadLine()?.Split(' ');

                    if(currentLine == null || currentLine.Length != 2) continue;

                    rowGameChoices.Add(rowCount, (CharToOption(currentLine[0][0]), CharToOption(currentLine[1][0])));
                    rowGameResults.Add(rowCount, (CharToOption(currentLine[0][0]), CharToResult(currentLine[1][0])));

                    rowCount++;

                } while (!fileReader.EndOfStream);
            }

            var allScores = rowGameChoices
                .ToDictionary(
                    x => x.Key, 
                    x => ScoreMatrix[x.Value.opponentsChoice][x.Value.yourChoice]
                    );

            var totalScore = allScores.Values.Sum();

            var allResults = rowGameResults
                .ToDictionary(
                    x => x.Key, 
                    x => ChoiceMatrix[x.Value.opponentsChoice][x.Value.desiredResult]
                    );

            var allScoresActual = rowGameChoices
                .ToDictionary(
                    x => x.Key,
                    x => ScoreMatrix[x.Value.opponentsChoice][allResults[x.Key]]
                );

            var totalScoreActual = allScoresActual.Values.Sum();

            sw.Stop();

            Console.WriteLine();

            Console.WriteLine("Total matches: " + allScores.Count);

            Console.WriteLine("Total score: " + totalScore);

            Console.WriteLine("Total score actual: " + totalScoreActual);

            Console.WriteLine($"Execution took {sw.Elapsed.TotalMilliseconds:0.}ms");

            Console.ReadKey();
        }
        
        private static GameOption CharToOption(char currentChar)
        {
            switch (currentChar)
            {
                case 'A' or 'X':
                    return GameOption.Rock;
                case 'B' or 'Y':
                    return GameOption.Paper;
                case 'C' or 'Z':
                    return GameOption.Scissor;
                default:
                    throw new NotImplementedException();
            }
        }

        private static GameResult CharToResult(char currentChar)
        {
            switch (currentChar)
            {
                case'X':
                    return GameResult.Lose;
                case 'Y':
                    return GameResult.Draw;
                case 'Z':
                    return GameResult.Win;
                default:
                    throw new NotImplementedException();
            }
        }
    }

}