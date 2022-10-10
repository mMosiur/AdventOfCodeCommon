using System.Numerics;

namespace AdventOfCode.Common.Geometry;

public readonly struct Point<T> : IEquatable<Point<T>>
	where T : INumber<T>
{
	public T X { get; }
	public T Y { get; }

	public static Point<T> Origin { get; } = new(T.Zero, T.Zero);

	public Point(T x, T y)
	{
		X = x;
		Y = y;
	}

	public bool Equals(Point<T> other) => X == other.X && Y == other.Y;
	public override bool Equals(object? obj) => obj is Point<T> point && Equals(point);
	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static bool operator ==(Point<T> left, Point<T> right) => left.Equals(right);
	public static bool operator !=(Point<T> left, Point<T> right) => !(left == right);

	public override string ToString() => $"Point({X}, {Y})";
}
