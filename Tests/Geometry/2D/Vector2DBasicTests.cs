using System.Numerics;
using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class Vector2DBasicTests
{
	private static void TestTVectorCreationAndBasicBehavior<T>() where T : INumber<T>
	{
		T one = T.One;
		T two = one + one;
		T three = two + one;
		T four = three + one;
		Vector2D<T> v1 = new(three, two);
		Vector2D<T> v2 = new(three, four);
		Assert.NotEqual(v2, v1);
		Vector2D<T> v3 = v1 with { Y = four };
		Assert.Equal(v2, v3);
		(T t1, T t2) = v3;
		Assert.Equal(three, t1);
		Assert.Equal(four, t2);
		Vector2D<T> zeros = new(T.Zero, T.Zero);
		Assert.Equal(zeros, Vector2D<T>.Zero);
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
	[InlineData(0, 0, 0, 0, 0, 0)]
	[InlineData(1, 2, 3, 4, 4, 6)]
	[InlineData(1, 2, -3, -4, -2, -2)]
	[InlineData(1, 2, 3, -4, 4, -2)]
	public void TestIntVector2DAdditionCorrect(int vector1X, int vector1Y, int vector2X, int vector2Y, int resultX, int resultY)
	{
		Vector2D<int> v1 = new(vector1X, vector1Y);
		Vector2D<int> v2 = new(vector2X, vector2Y);
		Vector2D<int> result = new(resultX, resultY);
		Assert.Equal(result, v1 + v2);
	}

	[Theory]
	[InlineData(0, 0, 0, 0, 0, 1)]
	[InlineData(1, 2, 3, 4, -2, -2)]
	[InlineData(1, 2, -3, -4, 4, 6)]
	public void TestIntVector2DAdditionFailed(int vector1X, int vector1Y, int vector2X, int vector2Y, int resultX, int resultY)
	{
		Vector2D<int> v1 = new(vector1X, vector1Y);
		Vector2D<int> v2 = new(vector2X, vector2Y);
		Vector2D<int> result = new(resultX, resultY);
		Assert.NotEqual(result, v1 + v2);
	}

	[Theory]
	[InlineData(0.0, 0.0, 0.0, 0.0, 0.0, 0.0)]
	[InlineData(1.0, 2.0, 3.0, 4.0, 4.0, 6.0)]
	[InlineData(1.0, 2.0, -3.0, -4.0, -2.0, -2.0)]
	[InlineData(1.0, 2.0, 3.0, -4.0, 4.0, -2.0)]
	public void TestFloatVector2DAdditionCorrect(float vector1X, float vector1Y, float vector2X, float vector2Y, float resultX, float resultY)
	{
		Vector2D<float> v1 = new(vector1X, vector1Y);
		Vector2D<float> v2 = new(vector2X, vector2Y);
		Vector2D<float> result = new(resultX, resultY);
		Assert.Equal(result, v1 + v2);
	}

	[Theory]
	[InlineData(0.0, 0.0, 0.0, 0.0, 0.0, 1.0)]
	[InlineData(1.0, 2.0, 3.0, 4.0, -2.0, -2.0)]
	[InlineData(1.0, 2.0, -3.0, -4.0, 4.0, 6.0)]
	public void TestFloatVector2DAdditionFailed(float vector1X, float vector1Y, float vector2X, float vector2Y, float resultX, float resultY)
	{
		Vector2D<float> v1 = new(vector1X, vector1Y);
		Vector2D<float> v2 = new(vector2X, vector2Y);
		Vector2D<float> result = new(resultX, resultY);
		Assert.NotEqual(result, v1 + v2);
	}
}
