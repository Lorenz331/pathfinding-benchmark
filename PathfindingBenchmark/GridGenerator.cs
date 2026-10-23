namespace PathfindingBenchmark;

public static class GridGenerator
{
    private static readonly Random Rnd = new();

    public static Grid Generate(Vector2Int gridSize, float wallPercent)
    {
        var grid = new Grid(gridSize);
        var path = RandomPath(grid.GridSize).ToHashSet();
        
        var totalCells = grid.GridSize.X * grid.GridSize.Y;
        var wallsLeft = (int)Math.Round(totalCells * wallPercent);
        var cellsLeft = totalCells - path.Count;

        for (var x = 0; x < gridSize.X; x++)
        {
            for (var y = 0; y < gridSize.Y; y++)
            {
                var position = new Vector2Int(x, y);
                if (path.Contains(position)) continue;

                if (Rnd.Next(cellsLeft) < wallsLeft)
                {
                    grid.SetIsWall(position, true);
                    wallsLeft--;
                }
                cellsLeft--;
            }
        }

        return grid;
    }

    private static IEnumerable<Vector2Int> RandomPath(Vector2Int gridSize)
    {
        var remainingX = gridSize.X - 1;
        var remainingY = gridSize.Y - 1;
        var position = new Vector2Int(0, 0);
        yield return position;

        for (var totalRemaining = remainingX + remainingY; totalRemaining > 0; totalRemaining--)
        {
            if (Rnd.Next(totalRemaining) < remainingX)
            {
                position += new Vector2Int(1, 0);
                remainingX--;
            }
            else
            {
                position += new Vector2Int(0, 1);
                remainingY--;
            }

            yield return position;
        }
    }
}