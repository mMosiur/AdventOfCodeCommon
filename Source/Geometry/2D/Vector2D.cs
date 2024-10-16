using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents a vector in 2D space
/// with two <typeparamref name="T"/> values.
/// </summary>
public readonly partial struct Vector2D<T> :
	IEquatable<Vector2D<T>>,
	IAdditiveIdentity<Vector2D<T>, Vector2D<T>>
	where T : unmanaged, INumber<T>
{

	/// <summary>The X value of the vector.</summary>
	public required T X { get; init; }

	/// <summary>The Y value of the vector.</summary>
	public required T Y { get; init; }

	/// <inheritdoc/>
	public static Vector2D<T> AdditiveIdentity => Zero;

	/// <summary>
	/// Gets vector whose components are all zeros.
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Zero_vector">Zero vector</seealso>
	public static Vector2D<T> Zero => new(T.Zero, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>X</c> dimension, that is
	/// a vector whose <see cref="X"/> component is one and all other components are all zeros.
	/// </summary>
	public static Vector2D<T> UnitX => new(T.One, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>Y</c> dimension, that is
	/// a vector whose <see cref="Y"/> component is one and all other components are all zeros.
	/// </summary>
	public static Vector2D<T> UnitY => new(T.Zero, T.One);

	/// <summary>
	/// Initializes a vector with the specified <paramref name="x"/>
	/// and <paramref name="y"/> values.
	/// </summary>
	/// <param name="x">The <c>X</c> value of the vector.</param>
	/// <param name="y">The <c>Y</c> value of the vector.</param>
	[SetsRequiredMembers]
	public Vector2D(T x, T y)
	{
		X = x;
		Y = y;
	}

	/// <summary>
	/// Initializes a vector as a displacement vector of the origin point <paramref name="from"/>
	/// and destination point <paramref name="to"/>.
	/// </summary>
	/// <param name="from">The initial point of the vector to be constructed.</param>
	/// <param name="to">The final point of the vector to be constructed.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Displacement_(vector)">Displacement (vector)</seealso>
	[SetsRequiredMembers]
	public Vector2D(Point2D<T> from, Point2D<T> to)
		: this(to.X - from.X, to.Y - from.Y)
	{
	}

	/// <summary>
	/// Initializes a vector as a copy of the specified <paramref name="vector"/>.
	/// </summary>
	/// <param name="vector">The vector to be copied.</param>
	[SetsRequiredMembers]
	public Vector2D(Vector2D<T> vector)
		: this(vector.X, vector.Y)
	{
	}

	/// <summary>
	/// Deconstruct this vector into provided <paramref name="x"/>
	/// and <paramref name="y"/> <see langword="out"/> parameters.
	/// </summary>
	public void Deconstruct(out T x, out T y)
	{
		x = X;
		y = Y;
	}

	/// <summary>
	/// Returns whether the specified vector is equal to this vector.
	/// </summary>
	/// <param name="other">The vector being compared to this one.</param>
	public bool Equals(Vector2D<T> other)
	{
		return X == other.X && Y == other.Y;
	}

	/// <summary>
	/// Determines whether the other object is also a <see cref="Vector2D{T}"/>
	/// and is equal to this one.
	/// </summary>
	/// <seealso cref="Equals(Vector2D{T})"/>
	public override bool Equals(object? obj)
	{
		return obj is Vector2D<T> vector && Equals(vector);
	}

	/// <summary>
	/// Returns the hash code of this vector.
	/// </summary>
	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y);
	}

	/// <summary>
	/// Returns a string that represents this vector.
	/// </summary>
	public override string ToString()
	{
		return $"[{X}, {Y}]";
	}

}
