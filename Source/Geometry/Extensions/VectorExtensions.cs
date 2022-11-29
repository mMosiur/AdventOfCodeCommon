using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Provides extension methods for vector types.
/// </summary>
public static class VectorExtensions
{

	/// <summary>
	/// Gets the length of the <paramref name="vector"/>.
	/// </summary>
	public static T GetLength<T>(this Vector2D<T> vector) where T : unmanaged, INumber<T>, IRootFunctions<T>
	{
		return T.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
	}

	/// <summary>
	/// Gets the length of the <paramref name="vector"/>.
	/// </summary>
	public static T GetLength<T>(this Vector3D<T> vector) where T : unmanaged, INumber<T>, IRootFunctions<T>
	{
		return T.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
	}

	/// <summary>
	/// Gets the length of the <paramref name="vector"/>.
	/// </summary>
	public static T GetLength<T>(this Vector4D<T> vector) where T : unmanaged, INumber<T>, IRootFunctions<T>
	{
		return T.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W);
	}

}
