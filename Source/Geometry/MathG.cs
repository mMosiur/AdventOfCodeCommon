using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Provides constants and static methods for common geometric mathematical functions.
/// </summary>
public static class MathG
{

	/// <summary>
	/// Gets the Manhattan distance based length of the <paramref name="vector"/> in 2D space.
	/// </summary>
	/// <param name="vector">The vector whose length is to be calculated.</param>
	/// <typeparam name="T">The value type of the vector.</typeparam>
	/// <seealso href="https://en.wikipedia.org/wiki/Manhattan_distance">Manhattan distance</seealso>
	public static T ManhattanLength<T>(Vector2D<T> vector)
		where T : unmanaged, INumber<T>
	{
		return T.Abs(vector.X) + T.Abs(vector.Y);
	}

	/// <summary>
	/// Gets the Manhattan distance based length of the <paramref name="vector"/> in 3D space.
	/// </summary>
	/// <param name="vector">The vector whose length is to be calculated.</param>
	/// <typeparam name="T">The value type of the vector.</typeparam>
	/// <seealso href="https://en.wikipedia.org/wiki/Manhattan_distance">Manhattan distance</seealso>
	public static T ManhattanLength<T>(Vector3D<T> vector)
		where T : unmanaged, INumber<T>
	{
		return T.Abs(vector.X) + T.Abs(vector.Y) + T.Abs(vector.Z);
	}

	/// <summary>
	/// Gets the Manhattan distance based length of the <paramref name="vector"/> in 4D space.
	/// </summary>
	/// <param name="vector">The vector whose length is to be calculated.</param>
	/// <typeparam name="T">The value type of the vector.</typeparam>
	/// <seealso href="https://en.wikipedia.org/wiki/Manhattan_distance">Manhattan distance</seealso>
	public static T ManhattanLength<T>(Vector4D<T> vector)
		where T : unmanaged, INumber<T>
	{
		return T.Abs(vector.X) + T.Abs(vector.Y) + T.Abs(vector.Z) + T.Abs(vector.W);
	}

	/// <summary>
	/// Gets the Manhattan distance between <paramref name="point1"/> and <paramref name="point2"/> in 2D space.
	/// </summary>
	/// <param name="point1">The first point of distance calculation.</param>
	/// <param name="point2">The second point of distance calculation.</param>
	/// <typeparam name="T">The value type of the points used.</typeparam>
	/// <seealso href="https://en.wikipedia.org/wiki/Manhattan_distance">Manhattan distance</seealso>
	public static T ManhattanDistance<T>(Point2D<T> point1, Point2D<T> point2)
		where T : unmanaged, INumber<T>
	{
		return T.Abs(point1.X - point2.X) + T.Abs(point1.Y - point2.Y);
	}

	/// <summary>
	/// Gets the Manhattan distance between <paramref name="point1"/> and <paramref name="point2"/> in 3D space.
	/// </summary>
	/// <param name="point1">The first point of distance calculation.</param>
	/// <param name="point2">The second point of distance calculation.</param>
	/// <typeparam name="T">The value type of the points used.</typeparam>
	/// <seealso href="https://en.wikipedia.org/wiki/Manhattan_distance">Manhattan distance</seealso>
	public static T ManhattanDistance<T>(Point3D<T> point1, Point3D<T> point2)
		where T : unmanaged, INumber<T>
	{
		return T.Abs(point1.X - point2.X) + T.Abs(point1.Y - point2.Y) + T.Abs(point1.Z - point2.Z);
	}

	/// <summary>
	/// Gets the Manhattan distance between <paramref name="point1"/> and <paramref name="point2"/> in 4D space.
	/// </summary>
	/// <param name="point1">The first point of distance calculation.</param>
	/// <param name="point2">The second point of distance calculation.</param>
	/// <typeparam name="T">The value type of the points used.</typeparam>
	/// <seealso href="https://en.wikipedia.org/wiki/Manhattan_distance">Manhattan distance</seealso>
	public static T ManhattanDistance<T>(Point4D<T> point1, Point4D<T> point2)
		where T : unmanaged, INumber<T>
	{
		return T.Abs(point1.X - point2.X) + T.Abs(point1.Y - point2.Y) + T.Abs(point1.Z - point2.Z) + T.Abs(point1.W - point2.W);
	}

}
