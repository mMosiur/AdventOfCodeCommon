using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

public readonly struct Vector3D<T> :
	IEquatable<Vector3D<T>>,
	IEqualityOperators<Vector3D<T>, Vector3D<T>, bool>,
	IAdditionOperators<Vector3D<T>, Vector3D<T>, Vector3D<T>>,
	IAdditionOperators<Vector3D<T>, Point3D<T>, Point3D<T>>,
	ISubtractionOperators<Vector3D<T>, Vector3D<T>, Vector3D<T>>,
	IMultiplyOperators<Vector3D<T>, T, Vector3D<T>>,
	IUnaryNegationOperators<Vector3D<T>, Vector3D<T>>,
	IUnaryPlusOperators<Vector3D<T>, Vector3D<T>>,
	IAdditiveIdentity<Vector3D<T>, Vector3D<T>>
	where T : INumber<T>
{
	public required T X { get; init; }
	public required T Y { get; init; }
	public required T Z { get; init; }

	public static Vector3D<T> AdditiveIdentity => Zero;
	public static Vector3D<T> Zero => new(T.Zero, T.Zero, T.Zero);
	public static Vector3D<T> UnitX => new(T.One, T.Zero, T.Zero);
	public static Vector3D<T> UnitY => new(T.Zero, T.One, T.Zero);
	public static Vector3D<T> UnitZ => new(T.Zero, T.Zero, T.One);


	[SetsRequiredMembers]
	public Vector3D(T x, T y, T z)
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

	public bool Equals(Vector3D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z;
	}

	#region Operators
	public static bool operator ==(Vector3D<T> left, Vector3D<T> right) => left.Equals(right);
	public static bool operator !=(Vector3D<T> left, Vector3D<T> right) => !(left == right);
	public static Vector3D<T> operator +(Vector3D<T> left, Vector3D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	public static Point3D<T> operator +(Vector3D<T> left, Point3D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	public static Vector3D<T> operator -(Vector3D<T> left, Vector3D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	public static Vector3D<T> operator *(Vector3D<T> left, T right) => new(left.X * right, left.Y * right, left.Z * right);
	public static Vector3D<T> operator *(T left, Vector3D<T> right) => right * left;
	public static Vector3D<T> operator -(Vector3D<T> value) => new(-value.X, -value.Y, -value.Z);
	public static Vector3D<T> operator +(Vector3D<T> value) => new(+value.X, +value.Y, +value.Z);
	#endregion

	public override bool Equals(object? obj)
	{
		return obj is Vector3D<T> vector && Equals(vector);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z);
	}

	public override string ToString()
	{
		return $"Vector[{X}, {Y}, {Z}]";
	}
}
