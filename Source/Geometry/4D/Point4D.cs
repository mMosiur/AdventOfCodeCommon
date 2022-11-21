using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

public readonly struct Point4D<T> :
	IEquatable<Point4D<T>>,
	IEqualityOperators<Point4D<T>, Point4D<T>, bool>,
	IAdditionOperators<Point4D<T>, Vector4D<T>, Point4D<T>>,
	ISubtractionOperators<Point4D<T>, Vector4D<T>, Point4D<T>>
	where T : INumber<T>
{
	public required T X { get; init; }
	public required T Y { get; init; }
	public required T Z { get; init; }
	public required T W { get; init; }

	public static Point4D<T> Origin { get; } = new(T.Zero, T.Zero, T.Zero, T.Zero);

	[SetsRequiredMembers]
	public Point4D(T x, T y, T z, T w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	public void Deconstruct(out T x, out T y, out T z, out T w)
	{
		x = X;
		y = Y;
		z = Z;
		w = W;
	}

	public bool Equals(Point4D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
	}

	#region Operators
	public static bool operator ==(Point4D<T> left, Point4D<T> right) => left.Equals(right);
	public static bool operator !=(Point4D<T> left, Point4D<T> right) => !(left == right);
	public static Point4D<T> operator +(Point4D<T> left, Vector4D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
	public static Point4D<T> operator -(Point4D<T> left, Vector4D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
	#endregion

	public override bool Equals(object? obj)
	{
		return obj is Point4D<T> point && Equals(point);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z, W);
	}

	public override string ToString()
	{
		return $"Point({X}, {Y}, {Z}, {W})";
	}
}
