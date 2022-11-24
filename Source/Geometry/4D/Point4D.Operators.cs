using System.Numerics;

namespace AdventOfCode.Common.Geometry;

// Operator definitions for <see cref="Point4D{T}"/>.
public readonly partial struct Point4D<T> :
	IEqualityOperators<Point4D<T>, Point4D<T>, bool>,
	IAdditionOperators<Point4D<T>, Vector4D<T>, Point4D<T>>,
	ISubtractionOperators<Point4D<T>, Vector4D<T>, Point4D<T>>
	where T : INumber<T>
{

	/// <inheritdoc/>
	public static bool operator ==(Point4D<T> left, Point4D<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Point4D<T> left, Point4D<T> right) => !(left == right);

	/// <inheritdoc/>
	public static Point4D<T> operator +(Point4D<T> left, Vector4D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

	/// <inheritdoc/>
	public static Point4D<T> operator -(Point4D<T> left, Vector4D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

}
