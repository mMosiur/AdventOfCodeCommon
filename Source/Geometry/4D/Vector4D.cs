using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

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
	public required T X { get; init; }
	public required T Y { get; init; }
	public required T Z { get; init; }
	public required T W { get; init; }

	public static Vector4D<T> AdditiveIdentity => Zero;
	public static Vector4D<T> Zero => new(T.Zero, T.Zero, T.Zero, T.Zero);
	public static Vector4D<T> UnitX => new(T.One, T.Zero, T.Zero, T.Zero);
	public static Vector4D<T> UnitY => new(T.Zero, T.One, T.Zero, T.Zero);
	public static Vector4D<T> UnitZ => new(T.Zero, T.Zero, T.One, T.Zero);
	public static Vector4D<T> UnitW => new(T.Zero, T.Zero, T.Zero, T.One);


	[SetsRequiredMembers]
	public Vector4D(T x, T y, T z, T w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	public void Deconstruct(out T x, out T y, out T z, out T w)
	{
		x = X;
		y = Y;
		z = Z;
		w = W;
	}

	public bool Equals(Vector4D<T> other)
	{
		return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
	}

	#region Operators
	public static bool operator ==(Vector4D<T> left, Vector4D<T> right) => left.Equals(right);
	public static bool operator !=(Vector4D<T> left, Vector4D<T> right) => !(left == right);
	public static Vector4D<T> operator +(Vector4D<T> left, Vector4D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
	public static Point4D<T> operator +(Vector4D<T> left, Point4D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
	public static Vector4D<T> operator -(Vector4D<T> left, Vector4D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
	public static Vector4D<T> operator /(Vector4D<T> left, T right) => new(left.X / right, left.Y / right, left.Z / right, left.W / right);
	public static Vector4D<T> operator *(Vector4D<T> left, T right) => new(left.X * right, left.Y * right, left.Z * right, left.W * right);
	public static Vector4D<T> operator *(T left, Vector4D<T> right) => right * left;
	public static Vector4D<T> operator -(Vector4D<T> value) => new(-value.X, -value.Y, -value.Z, -value.W);
	public static Vector4D<T> operator +(Vector4D<T> value) => new(+value.X, +value.Y, +value.Z, +value.W);
	#endregion

	public override bool Equals(object? obj)
	{
		return obj is Vector4D<T> vector && Equals(vector);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y, Z, W);
	}

	public override string ToString()
	{
		return $"Vector[{X}, {Y}, {Z}, {W}]";
	}
}
