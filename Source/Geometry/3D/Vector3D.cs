using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Represents a vector in 3D space
/// with three <typeparamref name="T"/> values.
/// </summary>
public readonly partial struct Vector3D<T> :
	IEquatable<Vector3D<T>>,
	IAdditiveIdentity<Vector3D<T>, Vector3D<T>>
	where T : unmanaged, INumber<T>
{

	/// <summary>The X value of the vector.</summary>
	public required T X { get; init; }

	/// <summary>The Y value of the vector.</summary>
	public required T Y { get; init; }

	/// <summary>The Z value of the vector.</summary>
	public required T Z { get; init; }

	/// <inheritdoc/>
	public static Vector3D<T> AdditiveIdentity => Zero;

	/// <summary>
	/// Gets vector whose components are all zeros.
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Zero_vector">Zero vector</seealso>
	public static Vector3D<T> Zero => new(T.Zero, T.Zero, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>X</c> dimension, that is
	/// a vector whose <see cref="X"/> component is one and all other components are all zeros.
	/// </summary>
	public static Vector3D<T> UnitX => new(T.One, T.Zero, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>Y</c> dimension, that is
	/// a vector whose <see cref="Y"/> component is one and all other components are all zeros.
	/// </summary>
	public static Vector3D<T> UnitY => new(T.Zero, T.One, T.Zero);

	/// <summary>
	/// Gets unit vector in <c>Z</c> dimension, that is
	/// a vector whose <see cref="Z"/> component is one and all other components are all zeros.
	/// </summary>
	public static Vector3D<T> UnitZ => new(T.Zero, T.Zero, T.One);

	/// <summary>
	/// Initializes a vector with the specified <paramref name="x"/>,
	/// <paramref name="y"/> and <paramref name="z"/> values.
	/// </summary>
	/// <param name="x">The <c>X</c> value of the vector.</param>
	/// <param name="y">The <c>Y</c> value of the vector.</param>
	/// <param name="z">The <c>Z</c> value of the vector.</param>
	[SetsRequiredMembers]
	public Vector3D(T x, T y, T z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	/// <summary>
	/// Initializes a vector as a displacement vector of the origin point <paramref name="from"/>
	/// and destination point <paramref name="to"/>.
	/// </summary>
	/// <param name="from">The initial point of the vector to be constructed.</param>
	/// <param name="to">The final point of the vector to be constructed.</param>
	/// <seealso href="https://en.wikipedia.org/wiki/Displacement_(vector)">Displacement (vector)</seealso>
	[SetsRequiredMembers]
	public Vector3D(Point3D<T> from, Point3D<T> to)
		: this(to.X - from.X, to.Y - from.Y, to.Z - from.Z)
	{
	}

	/// <summary>
	/// Initializes a vector as a copy of the specified <paramref name="vector"/>.
	/// </summary>
	/// <param name="vector">The vector to be copied.</param>
	[SetsRequiredMembers]
	public Vector3D(Vector3D<T> vector)
		: this(vector.X, vector.Y, vector.Z)
	{
	}

	/// <summary>
	/// Deconstruct this vector into provided <paramref name="x"/>,
	/// <paramref name="y"/> and <paramref name="z"/> <see langword="out"/> parameters.
	/// </summary>
	public void Deconstruct(out T x, out T y, out T z)
	{
		x = X;
		y = Y;
		z = Z;
	}

	/// <summary>
	/// Returns whether the specified vector is equal to this vector.
	/// </summary>
	/// <param name="other">The vector being compared to this one.</param>
	public bool Equals(Vector3D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z;
	}

	/// <summary>
	/// Determines whether the other object is also a <see cref="Vector3D{T}"/>
	/// and is equal to this one.
	/// </summary>
	/// <seealso cref="Equals(Vector3D{T})"/>
	public override bool Equals(object? obj)
	{
		return obj is Vector3D<T> vector && Equals(vector);
	}

	/// <summary>
	/// Returns the hash code of this vector.
	/// </summary>
	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z);
	}

	/// <summary>
	/// Returns a string that represents this vector.
	/// </summary>
	public override string ToString()
	{
		return $"[{X}, {Y}, {Z}]";
	}

}
