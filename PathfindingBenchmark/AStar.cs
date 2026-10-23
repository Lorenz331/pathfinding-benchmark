namespace PathfindingBenchmark;

public static class AStar
{
    public static List<Vector2Int> FindPath(
        Grid grid,
        Vector2Int start,
        Vector2Int goal,
        Func<Vector2Int, Vector2Int, int> heuristic)
    {
        var openList = new PriorityQueue<Vector2Int, (int, int, int)>();
        var gScores = new Dictionary<Vector2Int, int>();
        var predecessors = new Dictionary<Vector2Int, Vector2Int>();
        var visited = new HashSet<Vector2Int>();
        var counter = 0;

        gScores[start] = 0;
        var hScore = heuristic(start, goal);
        openList.Enqueue(start, (hScore, hScore, counter));

        while (openList.Count > 0)
        {
            var current = openList.Dequeue();

            if (!visited.Add(current))
            {
                continue;
            }

            if (current == goal)
            {
                return ReconstructPath(predecessors, current);
            }

            var neighbors = new HashSet<Vector2Int>();
            foreach (var offset in new Vector2Int[] { new(1, 0), new(0, 1), new(-1, 0), new(0, -1) })
            {
                var neighbor = current + offset;
                if (!grid.IsInGridBounds(neighbor) || grid.IsWall(neighbor) || visited.Contains(neighbor))
                {
                    continue;
                }

                neighbors.Add(neighbor);
            }

            foreach (var neighbor in neighbors)
            {
                var tentativeGScore = gScores[current] + 1;

                if (gScores.TryGetValue(neighbor, out var value) && value <= tentativeGScore)
                {
                    continue;
                }

                gScores[neighbor] = tentativeGScore;
                hScore = heuristic(neighbor, goal);
                predecessors[neighbor] = current;

                openList.Enqueue(neighbor, (tentativeGScore + hScore, hScore, counter++));
            }
        }

        return [];
    }

    private static List<Vector2Int> ReconstructPath(
        Dictionary<Vector2Int, Vector2Int> predecessors,
        Vector2Int current)
    {
        var path = new List<Vector2Int> { current };

        while (predecessors.ContainsKey(current))
        {
            current = predecessors[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}