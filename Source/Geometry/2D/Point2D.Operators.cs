using System.Numerics;

namespace AdventOfCode.Common.Geometry;

// Operator definitions for <see cref="Point2D{T}"/>.
public readonly partial struct Point2D<T> :
	IEqualityOperators<Point2D<T>, Point2D<T>, bool>,
	IAdditionOperators<Point2D<T>, Vector2D<T>, Point2D<T>>,
	ISubtractionOperators<Point2D<T>, Vector2D<T>, Point2D<T>>
	where T : unmanaged, INumber<T>
{

	/// <inheritdoc/>
	public static bool operator ==(Point2D<T> left, Point2D<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Point2D<T> left, Point2D<T> right) => !(left == right);

	/// <inheritdoc/>
	public static Point2D<T> operator +(Point2D<T> left, Vector2D<T> right) => new(left.X + right.X, left.Y + right.Y);

	/// <inheritdoc/>
	public static Point2D<T> operator -(Point2D<T> left, Vector2D<T> right) => new(left.X - right.X, left.Y - right.Y);

}
