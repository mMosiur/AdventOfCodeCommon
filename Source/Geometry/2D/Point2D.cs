using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents a point in 2D space
/// with two <typeparamref name="T"/> values.
/// </summary>
public readonly partial struct Point2D<T> :
	IEquatable<Point2D<T>>
	where T : INumber<T>
{

	/// <summary>The X value of the point.</summary>
	public required T X { get; init; }

	/// <summary>The Y value of the point.</summary>
	public required T Y { get; init; }

	/// <summary>
	/// Gets the point that is at the origin of the coordinate system.
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Origin_(mathematics)">Origin</seealso>
	public static Point2D<T> Origin => new(T.Zero, T.Zero);

	/// <summary>
	/// Initializes a point with the specified <paramref name="x"/>
	/// and <paramref name="y"/> values.
	/// </summary>
	[SetsRequiredMembers]
	public Point2D(T x, T y)
	{
		X = x;
		Y = y;
	}

	/// <summary>
	/// Deconstruct this point into provided <paramref name="x"/>
	/// and <paramref name="y"/> <see langword="out"/> parameters.
	/// </summary>
	public void Deconstruct(out T x, out T y)
	{
		x = X;
		y = Y;
	}

	/// <summary>
	/// Returns whether the specified point is equal to this point.
	/// </summary>
	/// <param name="other">The point being compared to this one.</param>
	public bool Equals(Point2D<T> other)
	{
		return X == other.X && Y == other.Y;
	}

	/// <summary>
	/// Determines whether the other object is also a <see cref="Point2D{T}"/>
	/// and is equal to this one.
	/// </summary>
	/// <seealso cref="Equals(Point2D{T})"/>
	public override bool Equals(object? obj)
	{
		return obj is Point2D<T> point && Equals(point);
	}

	/// <summary>
	/// Returns the hash code of this point.
	/// </summary>
	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y);
	}

	/// <summary>
	/// Returns a string representation of this point.
	/// </summary>
	public override string ToString()
	{
		return $"Point({X}, {Y})";
	}

}
