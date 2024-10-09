namespace AdventOfCode.Common.Tests.MathExtended;

public class GreatestCommonDivisorTests
{
	[Theory]
	[InlineData(0, 0, 0)]
	[InlineData(10, 0, 10)]
	[InlineData(0, 10, 10)]
	public void TestBasics(int a, int b, int expectedResult)
	{
		int result = Common.MathExtended.GreatestCommonDivisor(a, b);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(3392079986U, 2080089626U, 2U)]
	[InlineData(273672341U, 3912533700U, 1U)]
	[InlineData(1353048788U, 1969135932U, 4U)]
	[InlineData(1491301950U, 1356732645U, 15U)]
	[InlineData(3569727686U, 58056677U, 7U)]
	public void TestUInt32(uint a, uint b, uint expectedResult)
	{
		uint result = Common.MathExtended.GreatestCommonDivisor(a, b);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(339207998, -2080089626, 2)]
	[InlineData(273672341, 391253370, 1)]
	[InlineData(-1353048788, 1969135932, 4)]
	[InlineData(-1491301950, -1356732645, 15)]
	[InlineData(356972686, -58056677, 7)]
	public void TestInt32(int a, int b, int expectedResult)
	{
		int result = Common.MathExtended.GreatestCommonDivisor(a, b);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(190266297176832000UL, 10430732356495263744UL, 6144UL)]
	[InlineData(2040134905096275968UL, 5701159354248194048UL, 2048UL)]
	[InlineData(16611311494648745984UL, 7514969329383038976UL, 4096UL)]
	[InlineData(14863931409971066880UL, 7911906750992527360UL, 10240UL)]
	[InlineData(11777713923171739648UL, 1994469765110767616UL, 14336UL)]
	public void TestUInt64(ulong a, ulong b, ulong expectedResult)
	{
		ulong result = Common.MathExtended.GreatestCommonDivisor(a, b);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(190266297176832000L, -1043073235649526374L, 2L)]
	[InlineData(2040134905096275968L, 5701159354248194048L, 2048L)]
	[InlineData(-1661131149464875984L, 7514969329383038976L, 16L)]
	[InlineData(-1486931409971066880L, -7911906750992527360L, 10240L)]
	[InlineData(1177771392371739648L, -1994469765110767616L, 7168L)]
	public void TestInt64(long a, long b, long expectedResult)
	{
		long result = Common.MathExtended.GreatestCommonDivisor(a, b);
		Assert.Equal(expectedResult, result);
	}
}
