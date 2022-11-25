using System.Numerics;
using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Common.Tests.Geometry;

public class Point3DBasicTests
{
	private static void TestTPointCreationAndBasicBehavior<T>() where T : unmanaged, INumber<T>
	{
		T one = T.One;
		T two = one + one;
		T three = two + one;
		T four = three + one;
		Point3D<T> p1 = new(three, two, one);
		Point3D<T> p2 = new(three, four, one);
		Assert.NotEqual(p2, p1);
		Assert.True(p1 != p2);
		Point3D<T> p3 = p1 with { Y = four };
		Assert.Equal(p2, p3);
		Assert.True(p2 == p3);
		(T t1, T t2, T t3) = p3;
		Assert.Equal(three, t1);
		Assert.Equal(four, t2);
		Assert.Equal(one, t3);
		Point3D<T> zeros = new(T.Zero, T.Zero, T.Zero);
		Assert.Equal(zeros, Point3D<T>.Origin);
		Assert.True(zeros == Point3D<T>.Origin);
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
