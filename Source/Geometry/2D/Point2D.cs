using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

public readonly struct Point2D<T> :
	IEquatable<Point2D<T>>,
	IEqualityOperators<Point2D<T>, Point2D<T>, bool>,
	IAdditionOperators<Point2D<T>, Vector2D<T>, Point2D<T>>,
	ISubtractionOperators<Point2D<T>, Vector2D<T>, Point2D<T>>
	where T : INumber<T>
{
	public required T X { get; init; }
	public required T Y { get; init; }

	public static Point2D<T> Origin => new(T.Zero, T.Zero);

	[SetsRequiredMembers]
	public Point2D(T x, T y)
	{
		X = x;
		Y = y;
	}

	public void Deconstruct(out T x, out T y)
	{
		x = X;
		y = Y;
	}

	public bool Equals(Point2D<T> other)
	{
		return X == other.X && Y == other.Y;
	}

	#region Operators
	public static bool operator ==(Point2D<T> left, Point2D<T> right) => left.Equals(right);
	public static bool operator !=(Point2D<T> left, Point2D<T> right) => !(left == right);
	public static Point2D<T> operator +(Point2D<T> left, Vector2D<T> right) => new(left.X + right.X, left.Y + right.Y);
	public static Point2D<T> operator -(Point2D<T> left, Vector2D<T> right) => new(left.X - right.X, left.Y - right.Y);
	#endregion

	public override bool Equals(object? obj)
	{
		return obj is Point2D<T> point && Equals(point);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y);
	}

	public override string ToString()
	{
		return $"Point({X}, {Y})";
	}
}
