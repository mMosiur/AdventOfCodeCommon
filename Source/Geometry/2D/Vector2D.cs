using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

public readonly struct Vector2D<T> :
	IEquatable<Vector2D<T>>,
	IEqualityOperators<Vector2D<T>, Vector2D<T>, bool>,
	IAdditionOperators<Vector2D<T>, Vector2D<T>, Vector2D<T>>,
	IAdditionOperators<Vector2D<T>, Point2D<T>, Point2D<T>>,
	ISubtractionOperators<Vector2D<T>, Vector2D<T>, Vector2D<T>>,
	IMultiplyOperators<Vector2D<T>, T, Vector2D<T>>,
	IUnaryNegationOperators<Vector2D<T>, Vector2D<T>>,
	IUnaryPlusOperators<Vector2D<T>, Vector2D<T>>,
	IAdditiveIdentity<Vector2D<T>, Vector2D<T>>
	where T : INumber<T>
{
	public required T X { get; init; }
	public required T Y { get; init; }

	public static Vector2D<T> AdditiveIdentity => Zero;
	public static Vector2D<T> Zero => new(T.Zero, T.Zero);
	public static Vector2D<T> UnitX => new(T.One, T.Zero);
	public static Vector2D<T> UnitY => new(T.Zero, T.One);

	[SetsRequiredMembers]
	public Vector2D(T x, T y)
	{
		X = x;
		Y = y;
	}

	public void Deconstruct(out T x, out T y)
	{
		x = X;
		y = Y;
	}

	public bool Equals(Vector2D<T> other)
	{
		return X == other.X && Y == other.Y;
	}

	#region Operators
	public static bool operator ==(Vector2D<T> left, Vector2D<T> right) => left.Equals(right);
	public static bool operator !=(Vector2D<T> left, Vector2D<T> right) => !(left == right);
	public static Vector2D<T> operator +(Vector2D<T> left, Vector2D<T> right) => new(left.X + right.X, left.Y + right.Y);
	public static Point2D<T> operator +(Vector2D<T> left, Point2D<T> right) => new(left.X + right.X, left.Y + right.Y);
	public static Vector2D<T> operator -(Vector2D<T> left, Vector2D<T> right) => new(left.X - right.X, left.Y - right.Y);
	public static Vector2D<T> operator *(Vector2D<T> left, T right) => new(left.X * right, left.Y * right);
	public static Vector2D<T> operator *(T left, Vector2D<T> right) => right * left;
	public static Vector2D<T> operator -(Vector2D<T> value) => new(-value.X, -value.Y);
	public static Vector2D<T> operator +(Vector2D<T> value) => new(+value.X, +value.Y);
	#endregion

	public override bool Equals(object? obj)
	{
		return obj is Vector2D<T> vector && Equals(vector);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y);
	}

	public override string ToString()
	{
		return $"Vector[{X}, {Y}]";
	}
}
