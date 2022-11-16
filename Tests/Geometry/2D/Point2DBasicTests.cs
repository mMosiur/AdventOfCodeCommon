using System.Numerics;
using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class Point2DBasicTests
{
	private static void TestTPointCreationAndBasicBehavior<T>() where T : INumber<T>
	{
		T one = T.One;
		T two = one + one;
		T three = two + one;
		T four = three + one;
		Point2D<T> p1 = new(three, two);
		Point2D<T> p2 = new(three, four);
		Assert.NotEqual(p2, p1);
		Point2D<T> p3 = p1 with { Y = four };
		Assert.Equal(p2, p3);
		(T t1, T t2) = p3;
		Assert.Equal(three, t1);
		Assert.Equal(four, t2);
		Point2D<T> zeros = new(T.Zero, T.Zero);
		Assert.Equal(zeros, Point2D<T>.Origin);
	}

	[Fact] public void TestSbytePointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<sbyte>();
	[Fact] public void TestBytePointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<byte>();
	[Fact] public void TestShortPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<short>();
	[Fact] public void TestUshortPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<ushort>();
	[Fact] public void TestIntPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<int>();
	[Fact] public void TestUintPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<uint>();
	[Fact] public void TestLongPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<long>();
	[Fact] public void TestUlongPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<ulong>();
	[Fact] public void TestInt128PointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<Int128>();
	[Fact] public void TesUInt128PointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<UInt128>();
	[Fact] public void TestFloatPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<float>();
	[Fact] public void TestDoublePointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<double>();
	[Fact] public void TestDecimalPointCreationAndBasicBehavior() => TestTPointCreationAndBasicBehavior<decimal>();
}
