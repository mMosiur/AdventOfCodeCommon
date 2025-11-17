using System.Numerics;

namespace AdventOfCode.Common.Geometry;

/// <summary>
/// Provides extension methods for vector types.
/// </summary>
public static class VectorExtensions
{

	/// <summary>
	/// Extensions for <see cref="Vector2D{T}"/>.
	/// </summary>
	/// <param name="vector">The vector to be operated on.</param>
	/// <typeparam name="T">The numeric type of the vector components.</typeparam>
	extension<T>(Vector2D<T> vector) where T : unmanaged, INumber<T>, IRootFunctions<T>
	{
		/// <summary>
		/// Gets the length of the vector.
		/// </summary>
		public T GetLength()
		{
			return T.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
		}
	}

	/// <summary>
	/// Extensions for <see cref="Vector3D{T}"/>.
	/// </summary>
	/// <param name="vector">The vector to be operated on.</param>
	/// <typeparam name="T">The numeric type of the vector components.</typeparam>
	extension<T>(Vector3D<T> vector) where T : unmanaged, INumber<T>, IRootFunctions<T>
	{
		/// <summary>
		/// Gets the length of the vector.
		/// </summary>
		public T GetLength()
		{
			return T.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
		}
	}

	/// <summary>
	/// Extensions for <see cref="Vector4D{T}"/>.
	/// </summary>
	/// <param name="vector">The vector to be operated on.</param>
	/// <typeparam name="T">The numeric type of the vector components.</typeparam>
	extension<T>(Vector4D<T> vector) where T : unmanaged, INumber<T>, IRootFunctions<T>
	{
		/// <summary>
		/// Gets the length of the vector.
		/// </summary>
		public T GetLength()
		{
			return T.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W);
		}
	}

}
