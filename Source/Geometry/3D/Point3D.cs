using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents a point in 3D space
/// with three <typeparamref name="T"/> values.
/// </summary>
public readonly partial struct Point3D<T> :
	IEquatable<Point3D<T>>
	where T : unmanaged, INumber<T>
{

	/// <summary>The X value of the point.</summary>
	public required T X { get; init; }

	/// <summary>The Y value of the point.</summary>
	public required T Y { get; init; }

	/// <summary>The Z value of the point.</summary>
	public required T Z { get; init; }

	/// <summary>
	/// Gets the point that is at the origin of the coordinate system.
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Origin_(mathematics)">Origin</seealso>
	public static Point3D<T> Origin { get; } = new(T.Zero, T.Zero, T.Zero);

	/// <summary>
	/// Initializes a point with the specified <paramref name="x"/>,
	/// <paramref name="y"/> and <paramref name="z"/> values.
	/// </summary>
	[SetsRequiredMembers]
	public Point3D(T x, T y, T z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	/// <summary>
	/// Constructs a displacement vector from this point to the specified <paramref name="destination"/> point.
	/// </summary>
	/// <param name="destination">The destination point of the displacement vector.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Displacement_(vector)">Displacement vector</seealso>
	public Vector3D<T> VectorTo(Point3D<T> destination)
	{
		return new Vector3D<T>(this, destination);
	}

	/// <summary>
	/// Deconstruct this point into provided <paramref name="x"/>,
	/// <paramref name="y"/> and <paramref name="z"/> <see langword="out"/> parameters.
	/// </summary>
	public void Deconstruct(out T x, out T y, out T z)
	{
		x = X;
		y = Y;
		z = Z;
	}

	/// <summary>
	/// Returns whether the specified point is equal to this point.
	/// </summary>
	/// <param name="other">The point being compared to this one.</param>
	public bool Equals(Point3D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z;
	}

	/// <summary>
	/// Determines whether the other object is also a <see cref="Point3D{T}"/>
	/// and is equal to this one.
	/// </summary>
	/// <seealso cref="Equals(Point3D{T})"/>
	public override bool Equals(object? obj)
	{
		return obj is Point3D<T> point && Equals(point);
	}

	/// <summary>
	/// Returns the hash code of this point.
	/// </summary>
	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z);
	}

	/// <summary>
	/// Returns a string representation of this point.
	/// </summary>
	public override string ToString()
	{
		return $"({X}, {Y}, {Z})";
	}

}
