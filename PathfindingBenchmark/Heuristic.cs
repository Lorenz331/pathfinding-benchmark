namespace PathfindingBenchmark;

public static class Heuristic
{
    public static int ManhattanDistance(Vector2Int a, Vector2Int b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

    public static int NoHeuristic(Vector2Int a, Vector2Int b) => 0;
}