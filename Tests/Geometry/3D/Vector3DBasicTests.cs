using System.Numerics;
using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class Vector3DBasicTests
{
	private static void TestTVectorCreationAndBasicBehavior<T>() where T : unmanaged, INumber<T>
	{
		T one = T.One;
		T two = one + one;
		T three = two + one;
		T four = three + one;
		Vector3D<T> v1 = new(three, two, one);
		Vector3D<T> v2 = new(three, four, one);
		Assert.NotEqual(v2, v1);
		Assert.True(v1 != v2);
		Vector3D<T> v3 = v1 with { Y = four };
		Assert.Equal(v2, v3);
		Assert.True(v2 == v3);
		(T t1, T t2, T t3) = v3;
		Assert.Equal(three, t1);
		Assert.Equal(four, t2);
		Assert.Equal(one, t3);
		Vector3D<T> zeros = new(T.Zero, T.Zero, T.Zero);
		Assert.Equal(zeros, Vector3D<T>.Zero);
		Assert.True(zeros == Vector3D<T>.Zero);
	}

	[Fact] public void TestSbyteVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<sbyte>();
	[Fact] public void TestByteVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<byte>();
	[Fact] public void TestShortVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<short>();
	[Fact] public void TestUshortVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<ushort>();
	[Fact] public void TestIntVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<int>();
	[Fact] public void TestUintVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<uint>();
	[Fact] public void TestLongVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<long>();
	[Fact] public void TestUlongVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<ulong>();
	[Fact] public void TestInt128VectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<Int128>();
	[Fact] public void TesUInt128VectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<UInt128>();
	[Fact] public void TestFloatVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<float>();
	[Fact] public void TestDoubleVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<double>();
	[Fact] public void TestDecimalVectorCreationAndBasicBehavior() => TestTVectorCreationAndBasicBehavior<decimal>();

	[Theory]
	[InlineData(0, 0, 0, 0, 0, 0, 0, 0, 0)]
	[InlineData(1, 2, 3, 4, 5, 6, 5, 7, 9)]
	[InlineData(-1, -2, -3, -4, -5, -6, -5, -7, -9)]
	[InlineData(1, 2, 3, -4, -5, -6, -3, -3, -3)]
	public void TestIntVector3DAdditionCorrect(int vector1X, int vector1Y, int vector1Z, int vector2X, int vector2Y, int vector2Z, int resultX, int resultY, int resultZ)
	{
		Vector3D<int> v1 = new(vector1X, vector1Y, vector1Z);
		Vector3D<int> v2 = new(vector2X, vector2Y, vector2Z);
		Vector3D<int> result = new(resultX, resultY, resultZ);
		Assert.Equal(result, v1 + v2);
	}

	[Theory]
	[InlineData(0, 0, 0, 0, 0, 0, 0, 0, 1)]
	[InlineData(1, 2, 3, 4, 5, 6, 5, 7, 8)]
	[InlineData(-1, -2, -3, -4, -5, -6, -5, -7, -8)]
	[InlineData(1, 2, 3, -4, -5, -6, -3, -3, -4)]
	public void TestIntVector3DAdditionFailed(int vector1X, int vector1Y, int vector1Z, int vector2X, int vector2Y, int vector2Z, int resultX, int resultY, int resultZ)
	{
		Vector3D<int> v1 = new(vector1X, vector1Y, vector1Z);
		Vector3D<int> v2 = new(vector2X, vector2Y, vector2Z);
		Vector3D<int> result = new(resultX, resultY, resultZ);
		Assert.NotEqual(result, v1 + v2);
	}

	[Theory]
	[InlineData(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0)]
	[InlineData(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 5.0, 7.0, 9.0)]
	[InlineData(-1.0, -2.0, -3.0, -4.0, -5.0, -6.0, -5.0, -7.0, -9.0)]
	[InlineData(1.0, 2.0, 3.0, -4.0, -5.0, -6.0, -3.0, -3.0, -3.0)]
	public void TestFloatVector3DAdditionCorrect(float vector1X, float vector1Y, float vector1Z, float vector2X, float vector2Y, float vector2Z, float resultX, float resultY, float resultZ)
	{
		Vector3D<float> v1 = new(vector1X, vector1Y, vector1Z);
		Vector3D<float> v2 = new(vector2X, vector2Y, vector2Z);
		Vector3D<float> result = new(resultX, resultY, resultZ);
		Assert.Equal(result, v1 + v2);
	}

	[Theory]
	[InlineData(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0)]
	[InlineData(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 5.0, 7.0, 8.0)]
	[InlineData(-1.0, -2.0, -3.0, -4.0, -5.0, -6.0, -5.0, -7.0, -8.0)]
	[InlineData(1.0, 2.0, 3.0, -4.0, -5.0, -6.0, -3.0, -3.0, -4.0)]
	public void TestFloatVector3DAdditionFailed(float vector1X, float vector1Y, float vector1Z, float vector2X, float vector2Y, float vector2Z, float resultX, float resultY, float resultZ)
	{
		Vector3D<float> v1 = new(vector1X, vector1Y, vector1Z);
		Vector3D<float> v2 = new(vector2X, vector2Y, vector2Z);
		Vector3D<float> result = new(resultX, resultY, resultZ);
		Assert.NotEqual(result, v1 + v2);
	}
}
