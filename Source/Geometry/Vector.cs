using System;
using System.Numerics;

namespace AdventOfCode.Common.Geometry;

public readonly struct Vector<T> : IEquatable<Vector<T>>
	where T : INumber<T>
{
	public T X { get; }
	public T Y { get; }

	public static Vector<T> Zero { get; } = new(T.Zero, T.Zero);

	public Vector(T x, T y)
	{
		X = x;
		Y = y;
	}

	public static Vector<T> operator +(Vector<T> vec1, Vector<T> vec2) => new(vec1.X + vec2.X, vec1.Y + vec2.Y);
	public static Point<T> operator +(Vector<T> vector, Point<T> point) => new(vector.X + point.X, vector.Y + point.Y);
	public static Point<T> operator +(Point<T> point, Vector<T> vector) => vector + point;
	public static Vector<T> operator -(Vector<T> vec1, Vector<T> vec2) => new(vec1.X - vec2.X, vec1.Y - vec2.Y);
	public static Vector<T> operator *(Vector<T> vector, T scalar) => new(vector.X * scalar, vector.Y * scalar);
	public static Vector<T> operator *(T scalar, Vector<T> vector) => vector * scalar;
	public static Vector<T> operator -(Vector<T> vector) => new(-vector.X, -vector.Y);

	public bool Equals(Vector<T> other) => X == other.X && Y == other.Y;
	public override bool Equals(object? obj) => obj is Vector<T> vector && Equals(vector);
	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static bool operator ==(Vector<T> left, Vector<T> right) => left.Equals(right);
	public static bool operator !=(Vector<T> left, Vector<T> right) => !(left == right);

	public override string ToString() => $"Vector[{X}, {Y}]";
}
