namespace PathfindingBenchmark;

public class Grid
{
    public Vector2Int GridSize { get; private set; }

    private readonly HashSet<Vector2Int> _walls = [];

    public Grid(Vector2Int gridSize)
    {
        if (gridSize is { X: < 2, Y: < 2 })
        {
            throw new Exception("Grid is to small.");
        }

        GridSize = gridSize;
    }

    public bool IsInGridBounds(Vector2Int position)
    {
        return position is { X: >= 0, Y: >= 0 } &&
               position.X < GridSize.X &&
               position.Y < GridSize.Y;
    }

    public bool IsWall(Vector2Int position)
    {
        if (!IsInGridBounds(position))
        {
            throw new Exception("Position is not in grid");
        }

        return _walls.Contains(position);
    }

    public void SetIsWall(Vector2Int position, bool isWall)
    {
        if (!IsInGridBounds(position))
        {
            throw new Exception("Position is not in grid");
        }

        if (!isWall)
        {
            _walls.Remove(position);
        }
        else
        {
            _walls.Add(position);
        }
    }
}