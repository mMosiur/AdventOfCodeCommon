using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

public readonly struct Point3D<T> :
	IEquatable<Point3D<T>>,
	IEqualityOperators<Point3D<T>, Point3D<T>, bool>,
	IAdditionOperators<Point3D<T>, Vector3D<T>, Point3D<T>>,
	ISubtractionOperators<Point3D<T>, Vector3D<T>, Point3D<T>>
	where T : INumber<T>
{
	public required T X { get; init; }
	public required T Y { get; init; }
	public required T Z { get; init; }

	public static Point3D<T> Origin { get; } = new(T.Zero, T.Zero, T.Zero);

	[SetsRequiredMembers]
	public Point3D(T x, T y, T z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public void Deconstruct(out T x, out T y, out T z)
	{
		x = X;
		y = Y;
		z = Z;
	}

	public bool Equals(Point3D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z;
	}

	#region Operators
	public static bool operator ==(Point3D<T> left, Point3D<T> right) => left.Equals(right);
	public static bool operator !=(Point3D<T> left, Point3D<T> right) => !(left == right);
	public static Point3D<T> operator +(Point3D<T> left, Vector3D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	public static Point3D<T> operator -(Point3D<T> left, Vector3D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	#endregion

	public override bool Equals(object? obj)
	{
		return obj is Point3D<T> point && Equals(point);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z);
	}

	public override string ToString()
	{
		return $"Point({X}, {Y}, {Z})";
	}
}
