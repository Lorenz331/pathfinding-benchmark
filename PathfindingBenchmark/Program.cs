using System.Diagnostics;
using System.Text;

namespace PathfindingBenchmark;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Pathfinding Benchmark");

        var gridCount = 1000;
        var gridSizes = new[] { 25, 50, 75, 100 };
        var wallPercentages = new[] { 10, 20, 30, 40, 50 };

        var totalBenchmarks = gridSizes.Length * wallPercentages.Length;
        var currentBenchmark = 1;

        var start = new Vector2Int(0, 0);
        var stopwatchDijkstra = new Stopwatch();
        var stopwatchAStar = new Stopwatch();

        var sb = new StringBuilder();
        sb.AppendLine("Pathfinding Benchmark Results");
        sb.AppendLine("Grid Size;Wall Percentage;Avg Ms Dijkstra;Avg Ms A*");

        foreach (var gridSize in gridSizes)
        {
            var goal = new Vector2Int(gridSize - 1, gridSize - 1);
            foreach (var wallPercentage in wallPercentages)
            {
                Console.WriteLine($"Running Benchmark {currentBenchmark++}/{totalBenchmarks} (Grid Size = {gridSize}, Wall Percentage = {wallPercentage})");

                stopwatchDijkstra.Reset();
                stopwatchAStar.Reset();

                for (var i = 0; i < gridCount; i++)
                {
                    var grid = GridGenerator.Generate(new Vector2Int(gridSize, gridSize), wallPercentage);

                    stopwatchDijkstra.Start();
                    AStar.FindPath(grid, start, goal, Heuristic.NoHeuristic);
                    stopwatchDijkstra.Stop();

                    stopwatchAStar.Start();
                    AStar.FindPath(grid, start, goal, Heuristic.ManhattanDistance);
                    stopwatchAStar.Stop();

                    Console.Write($"\rCompleted Grid {i + 1} / {gridCount}");
                }

                Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");

                var avgDijkstra = stopwatchDijkstra.Elapsed.TotalMilliseconds / gridCount;
                var avgAStar = stopwatchAStar.Elapsed.TotalMilliseconds / gridCount;
                sb.AppendLine($"{gridSize};{wallPercentage};{avgDijkstra:F6};{avgAStar:F6}");
            }
        }

        var filePath = "benchmark_results.csv";
        File.WriteAllText(filePath, sb.ToString());

        Console.WriteLine();
        Console.WriteLine($"Results have been saved in {filePath}");
        Console.Write("Press any key to exit...");
        Console.ReadKey();
    }
}