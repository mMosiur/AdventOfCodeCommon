using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents a vector in 2D space
/// with two <typeparamref name="T"/> values.
/// </summary>
public readonly struct Vector2D<T> :
	IEquatable<Vector2D<T>>,
	IEqualityOperators<Vector2D<T>, Vector2D<T>, bool>,
	IAdditionOperators<Vector2D<T>, Vector2D<T>, Vector2D<T>>,
	IAdditionOperators<Vector2D<T>, Point2D<T>, Point2D<T>>,
	ISubtractionOperators<Vector2D<T>, Vector2D<T>, Vector2D<T>>,
	IDivisionOperators<Vector2D<T>, T, Vector2D<T>>,
	IMultiplyOperators<Vector2D<T>, T, Vector2D<T>>,
	IUnaryNegationOperators<Vector2D<T>, Vector2D<T>>,
	IUnaryPlusOperators<Vector2D<T>, Vector2D<T>>,
	IAdditiveIdentity<Vector2D<T>, Vector2D<T>>
	where T : INumber<T>
{

	/// <summary>The X value of the vector.</summary>
	public required T X { get; init; }

	/// <summary>The Y value of the vector.</summary>
	public required T Y { get; init; }

	/// <inheritdoc/>
	public static Vector2D<T> AdditiveIdentity => Zero;

	/// <summary>
	/// Gets vector whose components are all <see cref="T.Zero"/>.
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Zero_vector">Zero vector</seealso>
	public static Vector2D<T> Zero => new(T.Zero, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>X</c> dimension, that is
	/// a vector whose <see cref="X"/> component is <see cref="T.One"/> and all other components are all <see cref="T.Zero"/>.
	/// </summary>
	public static Vector2D<T> UnitX => new(T.One, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>Y</c> dimension, that is
	/// a vector whose <see cref="Y"/> component is <see cref="T.One"/> and all other components are all <see cref="T.Zero"/>.
	/// </summary>
	public static Vector2D<T> UnitY => new(T.Zero, T.One);

	/// <summary>
	/// Initializes a vector with the specified <paramref name="x"/>
	/// and <paramref name="y"/> values.
	/// </summary>
	[SetsRequiredMembers]
	public Vector2D(T x, T y)
	{
		X = x;
		Y = y;
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

	#region Operators

	/// <inheritdoc/>
	public static bool operator ==(Vector2D<T> left, Vector2D<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Vector2D<T> left, Vector2D<T> right) => !(left == right);

	/// <inheritdoc/>
	public static Vector2D<T> operator +(Vector2D<T> left, Vector2D<T> right) => new(left.X + right.X, left.Y + right.Y);

	/// <inheritdoc/>
	public static Point2D<T> operator +(Vector2D<T> left, Point2D<T> right) => new(left.X + right.X, left.Y + right.Y);

	/// <inheritdoc/>
	public static Vector2D<T> operator -(Vector2D<T> left, Vector2D<T> right) => new(left.X - right.X, left.Y - right.Y);

	/// <inheritdoc/>
	public static Vector2D<T> operator /(Vector2D<T> left, T right) => new(left.X / right, left.Y / right);

	/// <inheritdoc/>
	public static Vector2D<T> operator *(Vector2D<T> left, T right) => new(left.X * right, left.Y * right);

	/// <inheritdoc/>
	public static Vector2D<T> operator *(T left, Vector2D<T> right) => right * left;

	/// <inheritdoc/>
	public static Vector2D<T> operator -(Vector2D<T> value) => new(-value.X, -value.Y);

	/// <inheritdoc/>
	public static Vector2D<T> operator +(Vector2D<T> value) => new(+value.X, +value.Y);

	#endregion

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
		return $"Vector[{X}, {Y}]";
	}
}
