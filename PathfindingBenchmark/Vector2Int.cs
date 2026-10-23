namespace PathfindingBenchmark;

public struct Vector2Int(int x, int y) : IEquatable<Vector2Int>
{
    public int X { get; private set; } = x;
    public int Y { get; private set; } = y;

    public readonly bool Equals(Vector2Int other) => X == other.X && Y == other.Y;
    public readonly override bool Equals(object? obj) => obj is Vector2Int other && Equals(other);
    public readonly override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(Vector2Int a, Vector2Int b) => a.Equals(b);
    public static bool operator !=(Vector2Int a, Vector2Int b) => !a.Equals(b);

    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);
}