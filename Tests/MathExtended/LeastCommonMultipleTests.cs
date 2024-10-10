namespace AdventOfCode.Common.Tests.MathExtended;

public class LeastCommonMultipleTests
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(1213, 0)]
	[InlineData(0, 1213)]
	public void ThrowsWhenAnyArgIsZero(int a, int b)
	{
		Assert.Throws<ArgumentException>(() => Common.MathExtended.LeastCommonMultiple(a, b));
	}

	[Theory]
	[InlineData(1, 1, 1)]
	[InlineData(-1, 1, 1)]
	[InlineData(1, -1, 1)]
	[InlineData(-1, -1, 1)]
	[InlineData(8, 9, 72)]
	[InlineData(11, 5, 55)]
	[InlineData(15, 17, 255)]
	public void BasicTests(int a, int b, int expectedResult)
	{
		int result = Common.MathExtended.LeastCommonMultiple(a, b);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(6615, 70227, 17205615)]
	[InlineData(-1163, 85135, 99012005)]
	[InlineData(214359, -1248, 89173344)]
	[InlineData(-1176, -862486, 507141768)]
	public void TestInt32WithoutOverflow(int a, int b, int expectedResult)
	{
		long result = Common.MathExtended.LeastCommonMultiple(a, b);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(190266297, 1043073, 66153879070227)]
	[InlineData(20401349, -570115, 11631115085135)]
	[InlineData(-16611311, 7514969, 124833487214359)]
	[InlineData(-14863931, -7911906, 117602024862486)]
	public void TestInt32WithOverflowToInt64(int a, int b, long expectedResult)
	{
		long result = Common.MathExtended.LeastCommonMultiple<int, long>(a, b);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(new[] { 59, 80, 12 }, 14160)]
	[InlineData(new[] { 1, 2, -3, 4, -5 }, 60)]
	[InlineData(new[] { 5, 23, 98, 57, 14 }, 642390)]
	public void TestCollectionOfInt32WithoutOverflow(IEnumerable<int> numbers, int expectedResult)
	{
		int result = Common.MathExtended.LeastCommonMultiple(numbers);
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(new[] { 5, 23, 98, 57, 14, 44, 70, 15, 16, 446 }, 12606261360)]
	[InlineData(new[] { 7335, -44321, -66402 }, 2398547479230)]
	public void TestCollectionOfInt32WithOverflowToInt64(IEnumerable<int> numbers, long expectedResult)
	{
		long result = Common.MathExtended.LeastCommonMultiple<int, long>(numbers);
		Assert.Equal(expectedResult, result);
	}
}
