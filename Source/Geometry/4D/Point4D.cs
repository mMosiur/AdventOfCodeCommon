using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents a point in 4D space
/// with four <typeparamref name="T"/> values.
/// </summary>
public readonly struct Point4D<T> :
	IEquatable<Point4D<T>>,
	IEqualityOperators<Point4D<T>, Point4D<T>, bool>,
	IAdditionOperators<Point4D<T>, Vector4D<T>, Point4D<T>>,
	ISubtractionOperators<Point4D<T>, Vector4D<T>, Point4D<T>>
	where T : INumber<T>
{

	/// <summary>The X value of the point.</summary>
	public required T X { get; init; }

	/// <summary>The Y value of the point.</summary>
	public required T Y { get; init; }

	/// <summary>The Z value of the point.</summary>
	public required T Z { get; init; }

	/// <summary>The W value of the point.</summary>
	public required T W { get; init; }

	/// <summary>
	/// Gets the point that is at the origin of the coordinate system.
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Origin_(mathematics)">Origin</seealso>
	public static Point4D<T> Origin { get; } = new(T.Zero, T.Zero, T.Zero, T.Zero);

	/// <summary>
	/// Initializes a point with the specified <paramref name="x"/>, <paramref name="y"/>,
	/// <paramref name="z"/> and <paramref name="w"/> values.
	/// </summary>
	[SetsRequiredMembers]
	public Point4D(T x, T y, T z, T w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	/// <summary>
	/// Deconstruct this point into provided <paramref name="x"/>, <paramref name="y"/>,
	/// <paramref name="z"/> and <paramref name="w"/> <see langword="out"/> parameters.
	/// </summary>
	public void Deconstruct(out T x, out T y, out T z, out T w)
	{
		x = X;
		y = Y;
		z = Z;
		w = W;
	}

	/// <summary>
	/// Returns whether the specified point is equal to this point.
	/// </summary>
	/// <param name="other">The point being compared to this one.</param>
	public bool Equals(Point4D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
	}

	#region Operators

	/// <inheritdoc/>
	public static bool operator ==(Point4D<T> left, Point4D<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Point4D<T> left, Point4D<T> right) => !(left == right);

	/// <inheritdoc/>
	public static Point4D<T> operator +(Point4D<T> left, Vector4D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

	/// <inheritdoc/>
	public static Point4D<T> operator -(Point4D<T> left, Vector4D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

	#endregion

	/// <summary>
	/// Determines whether the other object is also a <see cref="Point4D{T}"/>
	/// and is equal to this one.
	/// </summary>
	/// <seealso cref="Equals(Point4D{T})"/>
	public override bool Equals(object? obj)
	{
		return obj is Point4D<T> point && Equals(point);
	}

	/// <summary>
	/// Returns the hash code of this point.
	/// </summary>
	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z, W);
	}

	/// <summary>
	/// Returns a string representation of this point.
	/// </summary>
	public override string ToString()
	{
		return $"Point({X}, {Y}, {Z}, {W})";
	}
}
