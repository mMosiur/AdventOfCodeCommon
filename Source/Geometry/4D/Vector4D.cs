using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents a vector in 4D space
/// with four <typeparamref name="T"/> values.
/// </summary>
public readonly struct Vector4D<T> :
	IEquatable<Vector4D<T>>,
	IEqualityOperators<Vector4D<T>, Vector4D<T>, bool>,
	IAdditionOperators<Vector4D<T>, Vector4D<T>, Vector4D<T>>,
	IAdditionOperators<Vector4D<T>, Point4D<T>, Point4D<T>>,
	ISubtractionOperators<Vector4D<T>, Vector4D<T>, Vector4D<T>>,
	IDivisionOperators<Vector4D<T>, T, Vector4D<T>>,
	IMultiplyOperators<Vector4D<T>, T, Vector4D<T>>,
	IUnaryNegationOperators<Vector4D<T>, Vector4D<T>>,
	IUnaryPlusOperators<Vector4D<T>, Vector4D<T>>,
	IAdditiveIdentity<Vector4D<T>, Vector4D<T>>
	where T : INumber<T>
{

	/// <summary>The X value of the vector.</summary>
	public required T X { get; init; }

	/// <summary>The Y value of the vector.</summary>
	public required T Y { get; init; }

	/// <summary>The Z value of the vector.</summary>
	public required T Z { get; init; }

	/// <summary>The W value of the vector.</summary>
	public required T W { get; init; }

	/// <inheritdoc/>
	public static Vector4D<T> AdditiveIdentity => Zero;

	/// <summary>
	/// Gets vector whose components are all <see cref="T.Zero"/>.
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Zero_vector">Zero vector</seealso>
	public static Vector4D<T> Zero => new(T.Zero, T.Zero, T.Zero, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>X</c> dimension, that is
	/// a vector whose <see cref="X"/> component is <see cref="T.One"/> and all other components are all <see cref="T.Zero"/>.
	/// </summary>
	public static Vector4D<T> UnitX => new(T.One, T.Zero, T.Zero, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>Y</c> dimension, that is
	/// a vector whose <see cref="Y"/> component is <see cref="T.One"/> and all other components are all <see cref="T.Zero"/>.
	/// </summary>
	public static Vector4D<T> UnitY => new(T.Zero, T.One, T.Zero, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>Z</c> dimension, that is
	/// a vector whose <see cref="Z"/> component is <see cref="T.One"/> and all other components are all <see cref="T.Zero"/>.
	/// </summary>
	public static Vector4D<T> UnitZ => new(T.Zero, T.Zero, T.One, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>W</c> dimension, that is
	/// a vector whose <see cref="W"/> component is <see cref="T.One"/> and all other components are all <see cref="T.Zero"/>.
	/// </summary>
	public static Vector4D<T> UnitW => new(T.Zero, T.Zero, T.Zero, T.One);

	/// <summary>
	/// Initializes a vector with the specified <paramref name="x"/>, <paramref name="y"/>,
	/// <paramref name="z"/> and <paramref name="w"/> values.
	/// </summary>
	[SetsRequiredMembers]
	public Vector4D(T x, T y, T z, T w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	/// <summary>
	/// Deconstruct this vector into provided <paramref name="x"/>, <paramref name="y"/>,
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
	/// Returns whether the specified vector is equal to this vector.
	/// </summary>
	/// <param name="other">The vector being compared to this one.</param>
	public bool Equals(Vector4D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
	}

	#region Operators

	/// <inheritdoc/>
	public static bool operator ==(Vector4D<T> left, Vector4D<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Vector4D<T> left, Vector4D<T> right) => !(left == right);

	/// <inheritdoc/>
	public static Vector4D<T> operator +(Vector4D<T> left, Vector4D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

	/// <inheritdoc/>
	public static Point4D<T> operator +(Vector4D<T> left, Point4D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

	/// <inheritdoc/>
	public static Vector4D<T> operator -(Vector4D<T> left, Vector4D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

	/// <inheritdoc/>
	public static Vector4D<T> operator /(Vector4D<T> left, T right) => new(left.X / right, left.Y / right, left.Z / right, left.W / right);

	/// <inheritdoc/>
	public static Vector4D<T> operator *(Vector4D<T> left, T right) => new(left.X * right, left.Y * right, left.Z * right, left.W * right);

	/// <inheritdoc/>
	public static Vector4D<T> operator *(T left, Vector4D<T> right) => right * left;

	/// <inheritdoc/>
	public static Vector4D<T> operator -(Vector4D<T> value) => new(-value.X, -value.Y, -value.Z, -value.W);

	/// <inheritdoc/>
	public static Vector4D<T> operator +(Vector4D<T> value) => new(+value.X, +value.Y, +value.Z, +value.W);

	#endregion

	/// <summary>
	/// Determines whether the other object is also a <see cref="Vector4D{T}"/>
	/// and is equal to this one.
	/// </summary>
	/// <seealso cref="Equals(Vector4D{T})"/>
	public override bool Equals(object? obj)
	{
		return obj is Vector4D<T> vector && Equals(vector);
	}

	/// <summary>
	/// Returns the hash code of this vector.
	/// </summary>
	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z, W);
	}

	/// <summary>
	/// Returns a string that represents this vector.
	/// </summary>
	public override string ToString()
	{
		return $"Vector[{X}, {Y}, {Z}, {W}]";
	}
}
