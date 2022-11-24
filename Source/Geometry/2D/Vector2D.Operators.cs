using System.Numerics;

namespace AdventOfCode.Common.Geometry;

// Operator definitions for <see cref="Vector2D{T}"/>.
public readonly partial struct Vector2D<T> :
	IEqualityOperators<Vector2D<T>, Vector2D<T>, bool>,
	IAdditionOperators<Vector2D<T>, Vector2D<T>, Vector2D<T>>,
	IAdditionOperators<Vector2D<T>, Point2D<T>, Point2D<T>>,
	ISubtractionOperators<Vector2D<T>, Vector2D<T>, Vector2D<T>>,
	IDivisionOperators<Vector2D<T>, T, Vector2D<T>>,
	IMultiplyOperators<Vector2D<T>, T, Vector2D<T>>,
	IUnaryNegationOperators<Vector2D<T>, Vector2D<T>>,
	IUnaryPlusOperators<Vector2D<T>, Vector2D<T>>
	where T : INumber<T>
{

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

}
