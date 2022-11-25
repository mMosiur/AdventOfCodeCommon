using System.Numerics;

namespace AdventOfCode.Common.Geometry;

// Operator definitions for <see cref="Point3D{T}"/>.
public readonly partial struct Point3D<T> :
	IEqualityOperators<Point3D<T>, Point3D<T>, bool>,
	IAdditionOperators<Point3D<T>, Vector3D<T>, Point3D<T>>,
	ISubtractionOperators<Point3D<T>, Vector3D<T>, Point3D<T>>
	where T : unmanaged, INumber<T>
{

	/// <inheritdoc/>
	public static bool operator ==(Point3D<T> left, Point3D<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Point3D<T> left, Point3D<T> right) => !(left == right);

	/// <inheritdoc/>
	public static Point3D<T> operator +(Point3D<T> left, Vector3D<T> right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

	/// <inheritdoc/>
	public static Point3D<T> operator -(Point3D<T> left, Vector3D<T> right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

}
